using Dreamflykit.Domain.Common;

namespace Dreamflykit.Domain.Geography;

public sealed class Season : AuditableEntity
{
    private Season()
    {
    }

    public Season(
        string name,
        int startMonth,
        int startDay,
        int endMonth,
        int endDay)
    {
        Validate(
            name,
            startMonth,
            startDay,
            endMonth,
            endDay);

        Name = name.Trim();
        StartMonth = startMonth;
        StartDay = startDay;
        EndMonth = endMonth;
        EndDay = endDay;
        IsActive = true;
    }

    public string Name { get; private set; } = string.Empty;

    public int StartMonth { get; private set; }

    public int StartDay { get; private set; }

    public int EndMonth { get; private set; }

    public int EndDay { get; private set; }

    public bool IsActive { get; private set; }

    public void Update(
        string name,
        int startMonth,
        int startDay,
        int endMonth,
        int endDay)
    {
        Validate(
            name,
            startMonth,
            startDay,
            endMonth,
            endDay);

        Name = name.Trim();
        StartMonth = startMonth;
        StartDay = startDay;
        EndMonth = endMonth;
        EndDay = endDay;

        MarkUpdated();
    }

    public void Activate()
    {
        IsActive = true;
        MarkUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkUpdated();
    }

    public bool ContainsDate(DateOnly date)
    {
        var currentDay = date.DayOfYear;

        var startDayOfYear = new DateOnly(
            date.Year,
            StartMonth,
            StartDay).DayOfYear;

        var endDayOfYear = new DateOnly(
            date.Year,
            EndMonth,
            EndDay).DayOfYear;

        if (startDayOfYear <= endDayOfYear)
        {
            return currentDay >= startDayOfYear
                && currentDay <= endDayOfYear;
        }

        // Handles seasons that cross the end of the year,
        // for example November → February.
        return currentDay >= startDayOfYear
            || currentDay <= endDayOfYear;
    }

    private static void Validate(
        string name,
        int startMonth,
        int startDay,
        int endMonth,
        int endDay)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(
                "Season name is required.");
        }

        ValidateDate(
            startMonth,
            startDay,
            "Season start date");

        ValidateDate(
            endMonth,
            endDay,
            "Season end date");
    }

    private static void ValidateDate(
        int month,
        int day,
        string fieldName)
    {
        if (month < 1 || month > 12)
        {
            throw new DomainException(
                $"{fieldName} month must be between 1 and 12.");
        }

        var maximumDay =
            DateTime.DaysInMonth(2024, month);

        if (day < 1 || day > maximumDay)
        {
            throw new DomainException(
                $"{fieldName} day is invalid.");
        }
    }
}