using Laba1.service;
using Laba1.src.view;
using Laba1.Src.util;

namespace Laba1;

/// <summary>
/// Главная форма приложения (пассивный View) для MVP.
/// </summary>
public partial class Form1 : Form, IHousingDepartmentFormView
{
    private const int START_WINDOW_HEIGHT = 451;
    private const int START_WINDOW_WIDTH = 526;
    private const int MAIN_WINDOW_HEIGHT = 723;
    private const int MAIN_WINDOW_WIDTH = 628;

    private readonly IHousingDepartmentPresenter _presenter;

    public event EventHandler? SaveRequested;
    public event EventHandler? ShowInfoToggleRequested;
    public event EventHandler? NextRequested;
    public event EventHandler? PreviousRequested;
    public event EventHandler? ExitRequested;
    public event EventHandler? FieldSelectionChanged;

    public int SelectedFieldIndex => comboBox_fields.SelectedIndex;
    public string DistrictInput => textBox_district.Text;
    public int HousingDepartmentNumberInput => (int)numericUpDown_housingDepartmentNumber.Value;
    public string ResidentNamesInput => textBox_residentName.Text;
    public string ResidentHouseNumbersInput => textBox_residentHouseNum.Text;
    public int PaidResidentsCountInput => (int)numericUpDown_paidResidentsCount.Value;
    public double TariffInput => (double)numericUpDown_tariff.Value;
    public decimal BalanceInput => numericUpDown_balance.Value;
    public int EmployeeCountInput => (int)numericUpDown_employeeCount.Value;

    public Form1()
    {
        InitializeComponent();

        _presenter = new HousingDepartmentPresenter(this, new ConsoleHousingDepartmentView());
        _presenter.Initialize();
    }

    /// <inheritdoc />
    public void ShowDepartmentInfo(string info)
    {
        label_show_info.Text = info;
    }

    /// <inheritdoc />
    public void ShowMainScreen()
    {
        panel2.Visible = true;
        tableLayoutPanel2.Visible = true;

        Size = new Size(MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);
        MaximumSize = new Size(MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);
        MinimumSize = new Size(MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);
    }

    /// <inheritdoc />
    public void ShowStartScreen()
    {
        panel2.Visible = false;
        tableLayoutPanel2.Visible = false;

        Size = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
        MaximumSize = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
        MinimumSize = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
    }

    /// <inheritdoc />
    public void UpdateInputVisibility(int selectedIndex)
    {
        textBox_district.Visible = false;
        numericUpDown_housingDepartmentNumber.Visible = false;
        textBox_residentHouseNum.Visible = false;
        textBox_residentName.Visible = false;
        numericUpDown_paidResidentsCount.Visible = false;
        numericUpDown_tariff.Visible = false;
        numericUpDown_balance.Visible = false;
        numericUpDown_employeeCount.Visible = false;

        button_save.Visible = selectedIndex >= 0;

        switch (selectedIndex)
        {
            case 0:
                textBox_district.Visible = true;
                break;

            case 1:
                numericUpDown_housingDepartmentNumber.Visible = true;
                break;

            case 2:
                textBox_residentHouseNum.Visible = true;
                textBox_residentName.Visible = true;
                break;

            case 3:
                numericUpDown_paidResidentsCount.Visible = true;
                break;

            case 4:
                numericUpDown_tariff.Visible = true;
                break;

            case 5:
                numericUpDown_balance.Visible = true;
                break;

            case 6:
                numericUpDown_employeeCount.Visible = true;
                break;
        }
    }

    /// <inheritdoc />
    public void ShowSavedStatus()
    {
        label_saved_status.Visible = true;
    }

    /// <inheritdoc />
    public void SetShowButtonText(string text)
    {
        button_show.Text = text;
    }

    /// <inheritdoc />
    public void ClearDepartmentInfo()
    {
        label_show_info.Text = string.Empty;
    }

    /// <inheritdoc />
    public void ShowError(string message)
    {
        NativeMessageBox.MessageBox(
            IntPtr.Zero,
            message,
            "Ошибка",
            NativeMessageBox.MB_OK | NativeMessageBox.MB_ICONERROR
        );
    }

    /// <inheritdoc />
    public void CloseView()
    {
        Close();
    }

    private void comboBox_fields_SelectedIndexChanged(object sender, EventArgs e)
    {
        FieldSelectionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void button_next_Click(object sender, EventArgs e)
    {
        NextRequested?.Invoke(this, EventArgs.Empty);
    }

    private void button_show_Click(object sender, EventArgs e)
    {
        ShowInfoToggleRequested?.Invoke(this, EventArgs.Empty);
    }

    private void button_save_Click(object sender, EventArgs e)
    {
        SaveRequested?.Invoke(this, EventArgs.Empty);
    }

    private void button_prev_Click(object sender, EventArgs e)
    {
        PreviousRequested?.Invoke(this, EventArgs.Empty);
    }

    private void button_exit_Click(object sender, EventArgs e)
    {
        ExitRequested?.Invoke(this, EventArgs.Empty);
    }
}
