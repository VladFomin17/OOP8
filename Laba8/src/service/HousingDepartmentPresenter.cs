using Laba1.src.view;
using Laba1.Src.Subject;

namespace Laba1.service;

/// <summary>
/// Презентер для управления объектом <see cref="HousingDepartment"/>.
/// В MVP презентер координирует модель и пассивные представления.
/// </summary>
public class HousingDepartmentPresenter : IHousingDepartmentPresenter
{
    /// <summary>
    /// Модель жилищного департамента.
    /// </summary>
    private readonly HousingDepartment _department;

    /// <summary>
    /// Основное WinForms-представление.
    /// </summary>
    private readonly IHousingDepartmentFormView? _view;

    /// <summary>
    /// Дополнительные представления (например, консольный вывод).
    /// </summary>
    private readonly List<IHousingDepartmentView> _secondaryViews = new();

    /// <summary>
    /// Флаг текущего отображения информации о департаменте на основной форме.
    /// </summary>
    private bool _isInfoVisible;

    /// <summary>
    /// Кэш последней сформированной строки с информацией о департаменте.
    /// </summary>
    private string _latestDepartmentInfo = string.Empty;

    /// <summary>
    /// Создаёт презентер, связывает его с формой и подписывается на события view.
    /// </summary>
    /// <param name="view">Основное пассивное представление.</param>
    public HousingDepartmentPresenter(IHousingDepartmentFormView view, HousingDepartment? department = null)
    {
        _department = department ?? HousingDepartment.Instance;
        _view = view;

        _view.SaveRequested += OnSaveRequested;
        _view.ShowInfoToggleRequested += OnShowInfoToggleRequested;
        _view.NextRequested += OnNextRequested;
        _view.PreviousRequested += OnPreviousRequested;
        _view.ExitRequested += OnExitRequested;
        _view.FieldSelectionChanged += OnFieldSelectionChanged;
    }

    /// <summary>
    /// Создаёт презентер для неформенных представлений (например, консольного view).
    /// </summary>
    public HousingDepartmentPresenter(HousingDepartment? department = null)
    {
        _department = department ?? HousingDepartment.Instance;
    }

    /// <inheritdoc />
    public void AttachView(IHousingDepartmentView view)
    {
        if (!_secondaryViews.Contains(view))
        {
            _secondaryViews.Add(view);
        }
    }

    /// <inheritdoc />
    public void Initialize()
    {
        if (_view is null)
        {
            _latestDepartmentInfo = GetDepartmentInfo();
            PublishToSecondaryViews(_latestDepartmentInfo);
            return;
        }

        _view.ShowStartScreen();
        _view.SetShowButtonText("Показать информацию");
        _view.ClearDepartmentInfo();
        _view.UpdateInputVisibility(_view.SelectedFieldIndex);

        _latestDepartmentInfo = GetDepartmentInfo();
        PublishToSecondaryViews(_latestDepartmentInfo);
    }

    /// <inheritdoc />
    public string GetDepartmentInfo()
    {
        return _department.ToString();
    }

    /// <inheritdoc />
    public void UpdateDistrict(string district)
    {
        if (!string.IsNullOrWhiteSpace(district))
        {
            _department.District = district;
            OnModelUpdated();
        }
    }

    /// <inheritdoc />
    public void UpdateHousingDepartmentNumber(int housingDepartmentNumber)
    {
        _department.HousingDepartmentNumber = housingDepartmentNumber;
        OnModelUpdated();
    }

    /// <inheritdoc />
    public void UpdateResidents(string names, string houseNumbers)
    {
        if (!string.IsNullOrWhiteSpace(names) && !string.IsNullOrWhiteSpace(houseNumbers))
        {
            _department.Residents = ParseResidents(names, houseNumbers);
            OnModelUpdated();
        }
    }

    /// <inheritdoc />
    public void UpdatePaidResidentsCount(int paidResidentsCount)
    {
        _department.PaidResidentsCount = paidResidentsCount;
        OnModelUpdated();
    }

    /// <inheritdoc />
    public void UpdateTariff(double tariff)
    {
        _department.Tariff = tariff;
        OnModelUpdated();
    }

    /// <inheritdoc />
    public void UpdateBalance(decimal balance)
    {
        _department.Balance = balance;
        OnModelUpdated();
    }

    /// <inheritdoc />
    public void UpdateEmployeeCount(int employeeCount)
    {
        _department.EmployeeCount = employeeCount;
        OnModelUpdated();
    }

    /// <summary>
    /// Обрабатывает команду сохранения введённых пользователем данных.
    /// </summary>
    private void OnSaveRequested(object? sender, EventArgs e)
    {
        if (_view is null)
        {
            return;
        }

        try
        {
            ApplyChangesFromView();
            _view.ShowSavedStatus();

            _isInfoVisible = true;
            _view.SetShowButtonText("Скрыть информацию");
            _view.ShowDepartmentInfo(_latestDepartmentInfo);
        }
        catch (Exception ex)
        {
            _view.ShowError(ex.Message);
        }
    }

    /// <summary>
    /// Переключает показ/скрытие информации о департаменте в основном представлении.
    /// </summary>
    private void OnShowInfoToggleRequested(object? sender, EventArgs e)
    {
        if (_view is null)
        {
            return;
        }

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

    /// <summary>
    /// Переводит форму на основной экран.
    /// </summary>
    private void OnNextRequested(object? sender, EventArgs e)
    {
        if (_view is null)
        {
            return;
        }

        _view.ShowMainScreen();
    }

    /// <summary>
    /// Возвращает форму на стартовый экран.
    /// </summary>
    private void OnPreviousRequested(object? sender, EventArgs e)
    {
        if (_view is null)
        {
            return;
        }

        _view.ShowStartScreen();
    }

    /// <summary>
    /// Закрывает основное представление.
    /// </summary>
    private void OnExitRequested(object? sender, EventArgs e)
    {
        if (_view is null)
        {
            return;
        }

        _view.CloseView();
    }

    /// <summary>
    /// Обновляет видимость полей ввода в зависимости от выбранного пользователем параметра.
    /// </summary>
    private void OnFieldSelectionChanged(object? sender, EventArgs e)
    {
        if (_view is null)
        {
            return;
        }

        _view.UpdateInputVisibility(_view.SelectedFieldIndex);
    }

    /// <summary>
    /// Применяет изменения из формы к модели на основе выбранного поля редактирования.
    /// </summary>
    private void ApplyChangesFromView()
    {
        if (_view is null)
        {
            return;
        }

        switch (_view.SelectedFieldIndex)
        {
            case 0:
                UpdateDistrict(_view.DistrictInput);
                break;
            case 1:
                UpdateHousingDepartmentNumber(_view.HousingDepartmentNumberInput);
                break;
            case 2:
                UpdateResidents(_view.ResidentNamesInput, _view.ResidentHouseNumbersInput);
                break;
            case 3:
                UpdatePaidResidentsCount(_view.PaidResidentsCountInput);
                break;
            case 4:
                UpdateTariff(_view.TariffInput);
                break;
            case 5:
                UpdateBalance(_view.BalanceInput);
                break;
            case 6:
                UpdateEmployeeCount(_view.EmployeeCountInput);
                break;
        }
    }

    /// <summary>
    /// Обновляет кэш, форму и дополнительные представления после изменения модели.
    /// </summary>
    private void OnModelUpdated()
    {
        _latestDepartmentInfo = GetDepartmentInfo();

        if (_isInfoVisible && _view is not null)
        {
            _view.ShowDepartmentInfo(_latestDepartmentInfo);
        }

        PublishToSecondaryViews(_latestDepartmentInfo);
    }

    /// <summary>
    /// Отправляет обновлённую информацию во все дополнительные представления.
    /// </summary>
    /// <param name="info">Строка с данными департамента.</param>
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
    /// <param name="residentNames">Имена жильцов, разделённые символом ';'.</param>
    /// <param name="residentNumberHouse">Номера домов, разделённые символом ';'.</param>
    /// <returns>Массив жильцов.</returns>
    /// <exception cref="IndexOutOfRangeException">
    /// Выбрасывается, если количество имён не совпадает с количеством номеров домов.
    /// </exception>
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


