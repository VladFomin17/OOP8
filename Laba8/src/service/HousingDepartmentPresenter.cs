using Laba1.src.view;
using Laba1.Src.Subject;

namespace Laba1.service;

/// <summary>
/// Презентер для управления объектом <see cref="HousingDepartment"/>.
/// В MVP презентер координирует модель и пассивное представление.
/// </summary>
public class HousingDepartmentPresenter : IHousingDepartmentPresenter
{
    private readonly HousingDepartment _department;
    private readonly IHousingDepartmentFormView _view;
    private readonly List<IHousingDepartmentView> _secondaryViews;

    private bool _isInfoVisible;
    private string _latestDepartmentInfo = string.Empty;

    public HousingDepartmentPresenter(
        IHousingDepartmentFormView view,
        params IHousingDepartmentView[] secondaryViews)
    {
        _department = HousingDepartment.Instance;
        _view = view;
        _secondaryViews = secondaryViews.ToList();

        _view.SaveRequested += OnSaveRequested;
        _view.ShowInfoToggleRequested += OnShowInfoToggleRequested;
        _view.NextRequested += OnNextRequested;
        _view.PreviousRequested += OnPreviousRequested;
        _view.ExitRequested += OnExitRequested;
        _view.FieldSelectionChanged += OnFieldSelectionChanged;
    }

    /// <inheritdoc />
    public void Initialize()
    {
        _view.ShowStartScreen();
        _view.SetShowButtonText("Показать информацию");
        _view.ClearDepartmentInfo();
        _view.UpdateInputVisibility(_view.SelectedFieldIndex);

        _latestDepartmentInfo = GetDepartmentInfo();
        PublishToSecondaryViews(_latestDepartmentInfo);
    }

    private void OnSaveRequested(object? sender, EventArgs e)
    {
        try
        {
            ApplyChangesFromView();
            _view.ShowSavedStatus();

            _latestDepartmentInfo = GetDepartmentInfo();
            PublishToSecondaryViews(_latestDepartmentInfo);

            _isInfoVisible = true;
            _view.SetShowButtonText("Скрыть информацию");
            _view.ShowDepartmentInfo(_latestDepartmentInfo);
        }
        catch (Exception ex)
        {
            _view.ShowError(ex.Message);
        }
    }

    private void OnShowInfoToggleRequested(object? sender, EventArgs e)
    {
        if (!_isInfoVisible)
        {
            if (string.IsNullOrWhiteSpace(_latestDepartmentInfo))
            {
                _latestDepartmentInfo = GetDepartmentInfo();
            }

            _isInfoVisible = true;
            _view.SetShowButtonText("Скрыть информацию");
            _view.ShowDepartmentInfo(_latestDepartmentInfo);
            return;
        }

        _isInfoVisible = false;
        _view.SetShowButtonText("Показать информацию");
        _view.ClearDepartmentInfo();
    }

    private void OnNextRequested(object? sender, EventArgs e)
    {
        _view.ShowMainScreen();
    }

    private void OnPreviousRequested(object? sender, EventArgs e)
    {
        _view.ShowStartScreen();
    }

    private void OnExitRequested(object? sender, EventArgs e)
    {
        _view.CloseView();
    }

    private void OnFieldSelectionChanged(object? sender, EventArgs e)
    {
        _view.UpdateInputVisibility(_view.SelectedFieldIndex);
    }

    private void ApplyChangesFromView()
    {
        switch (_view.SelectedFieldIndex)
        {
            case 0:
                if (!string.IsNullOrWhiteSpace(_view.DistrictInput))
                {
                    _department.District = _view.DistrictInput;
                }

                break;

            case 1:
                _department.HousingDepartmentNumber = _view.HousingDepartmentNumberInput;
                break;

            case 2:
                if (!string.IsNullOrWhiteSpace(_view.ResidentNamesInput) &&
                    !string.IsNullOrWhiteSpace(_view.ResidentHouseNumbersInput))
                {
                    _department.Residents = ParseResidents(_view.ResidentNamesInput, _view.ResidentHouseNumbersInput);
                }

                break;

            case 3:
                _department.PaidResidentsCount = _view.PaidResidentsCountInput;
                break;

            case 4:
                _department.Tariff = _view.TariffInput;
                break;

            case 5:
                _department.Balance = _view.BalanceInput;
                break;

            case 6:
                _department.EmployeeCount = _view.EmployeeCountInput;
                break;
        }
    }

    private string GetDepartmentInfo()
    {
        return _department.ToString();
    }

    private void PublishToSecondaryViews(string info)
    {
        foreach (IHousingDepartmentView secondaryView in _secondaryViews)
        {
            secondaryView.ShowDepartmentInfo(info);
        }
    }

    /// <summary>
    /// Преобразует строки с данными жильцов в массив объектов <see cref="Resident"/>.
    /// </summary>
    private Resident[] ParseResidents(string residentNames, string residentNumberHouse)
    {
        string[] splitNames = residentNames.Split(';');
        string[] splitNumber = residentNumberHouse.Split(';');

        if (splitNames.Length != splitNumber.Length)
        {
            throw new IndexOutOfRangeException("Количество имен не совпадает с количеством номеров домов");
        }

        Resident[] residents = new Resident[splitNames.Length];

        for (int i = 0; i < splitNames.Length; i++)
        {
            int num = int.Parse(splitNumber[i]);
            residents[i] = new Resident(num, splitNames[i]);
        }

        return residents;
    }
}
