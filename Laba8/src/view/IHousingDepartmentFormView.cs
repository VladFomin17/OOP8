namespace Laba1.src.view;

/// <summary>
/// Контракт пассивного WinForms-представления для MVP.
/// </summary>
public interface IHousingDepartmentFormView : IHousingDepartmentView
{
    event EventHandler? SaveRequested;
    event EventHandler? ShowInfoToggleRequested;
    event EventHandler? NextRequested;
    event EventHandler? PreviousRequested;
    event EventHandler? ExitRequested;
    event EventHandler? FieldSelectionChanged;

    int SelectedFieldIndex { get; }
    string DistrictInput { get; }
    int HousingDepartmentNumberInput { get; }
    string ResidentNamesInput { get; }
    string ResidentHouseNumbersInput { get; }
    int PaidResidentsCountInput { get; }
    double TariffInput { get; }
    decimal BalanceInput { get; }
    int EmployeeCountInput { get; }

    void ShowMainScreen();
    void ShowStartScreen();
    void UpdateInputVisibility(int selectedIndex);
    void ShowSavedStatus();
    void SetShowButtonText(string text);
    void ClearDepartmentInfo();
    void ShowError(string message);
    void CloseView();
}
