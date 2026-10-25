document.addEventListener("DOMContentLoaded", () => {
    // 1. Mobile Menu Drawer Toggle
    const menuButton = document.getElementById("mobileMenuButton");
    const mobileNavigation = document.getElementById("mobileNavigation");

    if (menuButton && mobileNavigation) {
        menuButton.addEventListener("click", () => {
            const isOpen = mobileNavigation.classList.toggle("is-open");
            menuButton.setAttribute("aria-expanded", isOpen.toString());
        });
    }

    // 2. Refresh Header Badges (Cart & Wishlist)
    updateHeaderCounters();

    // 2b. Bank of Baroda Style Dynamic Sticky Header Scroll Collapse
    const header = document.querySelector(".dk-header");
    if (header) {
        let ticking = false;
        const onScroll = () => {
            if (!ticking) {
                window.requestAnimationFrame(() => {
                    const scrolled = window.scrollY > 35;
                    header.classList.toggle("is-scrolled", scrolled);
                    ticking = false;
                });
                ticking = true;
            }
        };
        window.addEventListener("scroll", onScroll, { passive: true });
        onScroll(); // Run immediately on page load
    }

    // 3. Wishlist Heart Toggle Listener (Event Delegation)
    document.addEventListener("click", async (e) => {
        const btn = e.target.closest(".dk-wishlist-toggle-btn");
        if (!btn) return;

        e.preventDefault();
        e.stopPropagation();

        const variantId = btn.getAttribute("data-variant-id");
        if (!variantId) return;

        // Get CSRF token
        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        const token = tokenInput ? tokenInput.value : "";

        try {
            const formData = new FormData();
            formData.append("productVariantId", variantId);
            if (token) {
                formData.append("__RequestVerificationToken", token);
            }

            const response = await fetch("/Wishlist/Toggle", {
                method: "POST",
                body: formData,
                headers: {
                    "X-Requested-With": "XMLHttpRequest"
                }
            });

            if (response.redirected) {
                window.location.href = response.url;
                return;
            }

            if (response.status === 401) {
                window.location.href = "/Account/Login?returnUrl=" + encodeURIComponent(window.location.pathname);
                return;
            }

            const data = await response.json();
            if (data.success) {
                btn.classList.toggle("is-active", data.isInWishlist);
                const countBadge = document.getElementById("headerWishlistCount");
                if (countBadge) {
                    countBadge.textContent = data.count;
                }
                showToast(data.message);
            }
        } catch (err) {
            console.error("Wishlist toggle error:", err);
        }
    });

    async function updateHeaderCounters() {
        try {
            // Cart count
            const cartRes = await fetch("/Cart/Count");
            if (cartRes.ok) {
                const cartData = await cartRes.json();
                const cartBadge = document.querySelector(".dk-cart-count");
                if (cartBadge && typeof cartData.count === "number") {
                    cartBadge.textContent = cartData.count;
                }
            }

            // Wishlist count
            const wishRes = await fetch("/Wishlist/Count");
            if (wishRes.ok) {
                const wishData = await wishRes.json();
                const wishBadge = document.getElementById("headerWishlistCount");
                if (wishBadge && typeof wishData.count === "number") {
                    wishBadge.textContent = wishData.count;
                }
            }
        } catch {
            // Guest or network offline
        }
    }

    function showToast(message) {
        let toast = document.getElementById("dk-toast");
        if (!toast) {
            toast = document.createElement("div");
            toast.id = "dk-toast";
            toast.style.cssText = `
                position: fixed;
                bottom: 24px;
                right: 24px;
                background: #0b224e;
                color: #fff;
                padding: 12px 20px;
                border-radius: 8px;
                box-shadow: 0 4px 16px rgba(0,0,0,0.25);
                font-size: 14px;
                font-weight: 600;
                z-index: 9999;
                transition: opacity 0.3s ease, transform 0.3s ease;
                transform: translateY(10px);
                opacity: 0;
            `;
            document.body.appendChild(toast);
        }

        toast.textContent = message;
        toast.style.opacity = "1";
        toast.style.transform = "translateY(0)";

        setTimeout(() => {
            toast.style.opacity = "0";
            toast.style.transform = "translateY(10px)";
        }, 2500);
    }

    // 4. Live Search Autocomplete Dropdown
    const searchInput = document.getElementById("dk-search-input");
    const autocompleteDropdown = document.getElementById("dk-search-autocomplete");
    let autocompleteDebounceTimer = null;
    let selectedIndex = -1;

    if (searchInput && autocompleteDropdown) {
        searchInput.addEventListener("input", (e) => {
            clearTimeout(autocompleteDebounceTimer);
            const val = e.target.value.trim();

            if (val.length < 2) {
                hideAutocomplete();
                return;
            }

            autocompleteDebounceTimer = setTimeout(async () => {
                try {
                    const res = await fetch(`/products/autocomplete?q=${encodeURIComponent(val)}`);
                    if (!res.ok) {
                        hideAutocomplete();
                        return;
                    }

                    const data = await res.json();
                    renderAutocomplete(data, val);
                } catch {
                    hideAutocomplete();
                }
            }, 220);
        });

        searchInput.addEventListener("keydown", (e) => {
            const items = autocompleteDropdown.querySelectorAll(".dk-autocomplete-item");
            if (!items.length || autocompleteDropdown.style.display === "none") return;

            if (e.key === "ArrowDown") {
                e.preventDefault();
                selectedIndex = (selectedIndex + 1) % items.length;
                updateSelectedItem(items);
            } else if (e.key === "ArrowUp") {
                e.preventDefault();
                selectedIndex = (selectedIndex - 1 + items.length) % items.length;
                updateSelectedItem(items);
            } else if (e.key === "Enter" && selectedIndex >= 0 && items[selectedIndex]) {
                e.preventDefault();
                items[selectedIndex].click();
            } else if (e.key === "Escape") {
                hideAutocomplete();
            }
        });

        document.addEventListener("click", (e) => {
            if (!e.target.closest(".dk-search-box-wrapper")) {
                hideAutocomplete();
            }
        });
    }

    function renderAutocomplete(data, currentQuery) {
        if (!autocompleteDropdown) return;

        const hasProducts = data.products && data.products.length > 0;
        const hasCategories = data.categories && data.categories.length > 0;

        if (!hasProducts && !hasCategories) {
            hideAutocomplete();
            return;
        }

        selectedIndex = -1;
        let html = "";

        if (hasCategories) {
            html += `<div class="dk-autocomplete-section">
                <div class="dk-autocomplete-section-title">In Categories</div>`;
            data.categories.forEach(cat => {
                html += `
                    <a href="/products?category=${encodeURIComponent(cat.slug)}" class="dk-autocomplete-item dk-autocomplete-category-item">
                        <span class="dk-autocomplete-category-icon">🔍</span>
                        <span>"${escapeHtml(currentQuery)}" in <strong>${escapeHtml(cat.name)}</strong></span>
                    </a>
                `;
            });
            html += `</div>`;
        }

        if (hasProducts) {
            html += `<div class="dk-autocomplete-section">
                <div class="dk-autocomplete-section-title">Products</div>`;
            data.products.forEach(p => {
                const img = p.imageUrl || "/images/products/product-placeholder.webp";
                const priceFormatted = "₹" + Number(p.sellingPrice).toLocaleString();
                const mrpFormatted = p.mrp > p.sellingPrice ? "₹" + Number(p.mrp).toLocaleString() : "";

                html += `
                    <a href="/product/${encodeURIComponent(p.slug)}" class="dk-autocomplete-item">
                        <img src="${img}" alt="${escapeHtml(p.name)}" class="dk-autocomplete-product-thumb" />
                        <div class="dk-autocomplete-product-info">
                            <div class="dk-autocomplete-product-name">${escapeHtml(p.name)}</div>
                            <div class="dk-autocomplete-product-meta">
                                <span class="dk-autocomplete-product-price">${priceFormatted}</span>
                                ${mrpFormatted ? `<span class="dk-autocomplete-product-mrp">${mrpFormatted}</span>` : ""}
                                ${p.categoryName ? `<span class="dk-autocomplete-product-cat">${escapeHtml(p.categoryName)}</span>` : ""}
                            </div>
                        </div>
                    </a>
                `;
            });
            html += `</div>`;
        }

        autocompleteDropdown.innerHTML = html;
        autocompleteDropdown.style.display = "block";
    }

    function hideAutocomplete() {
        if (autocompleteDropdown) {
            autocompleteDropdown.style.display = "none";
            autocompleteDropdown.innerHTML = "";
            selectedIndex = -1;
        }
    }

    function updateSelectedItem(items) {
        items.forEach((item, idx) => {
            item.classList.toggle("selected", idx === selectedIndex);
            if (idx === selectedIndex) {
                item.scrollIntoView({ block: "nearest" });
            }
        });
    }

    function escapeHtml(str) {
        if (!str) return "";
        return str.replace(/[&<>"']/g, (m) => {
            switch (m) {
                case "&": return "&amp;";
                case "<": return "&lt;";
                case ">": return "&gt;";
                case '"': return "&quot;";
                case "'": return "&#039;";
                default: return m;
            }
        });
    }

    // 5. Mobile Filter Drawer Controls
    const openFilterBtn = document.getElementById("dk-mobile-filter-open");
    const closeFilterBtn = document.getElementById("dk-mobile-filter-close");
    const filterSidebar = document.getElementById("dk-filter-sidebar");
    const filterBackdrop = document.getElementById("dk-filter-backdrop");

    if (openFilterBtn && filterSidebar && filterBackdrop) {
        openFilterBtn.addEventListener("click", () => {
            filterSidebar.classList.add("open");
            filterBackdrop.classList.add("open");
            document.body.style.overflow = "hidden";
        });

        const closeFilter = () => {
            filterSidebar.classList.remove("open");
            filterBackdrop.classList.remove("open");
            document.body.style.overflow = "";
        };

        if (closeFilterBtn) {
            closeFilterBtn.addEventListener("click", closeFilter);
        }
        filterBackdrop.addEventListener("click", closeFilter);
    }
});