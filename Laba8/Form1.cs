using Laba1.service;
using Laba1.src.view;
using Laba1.Src.util;

namespace Laba1;

/// <summary>
/// Главная форма приложения (View) для MVP.
/// </summary>
public partial class Form1 : Form, IHousingDepartmentView
{
    private const int START_WINDOW_HEIGHT = 451;
    private const int START_WINDOW_WIDTH = 526;
    private const int MAIN_WINDOW_HEIGHT = 723;
    private const int MAIN_WINDOW_WIDTH = 628;

    private bool _isShowedInfo;
    private string _latestDepartmentInfo = string.Empty;

    private readonly IHousingDepartmentPresenter _presenter;

    public Form1()
    {
        InitializeComponent();

        Size = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
        MaximumSize = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
        MinimumSize = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);

        panel2.Visible = false;
        tableLayoutPanel2.Visible = false;

        _presenter = new HousingDepartmentPresenter();
        _presenter.AttachView(this);
        _presenter.AttachView(new ConsoleHousingDepartmentView());

        _presenter.RefreshViews();
    }

    /// <inheritdoc />
    public void ShowDepartmentInfo(string info)
    {
        _latestDepartmentInfo = info;

        if (_isShowedInfo)
        {
            label_show_info.Text = info;
        }
    }

    private void FillValues()
    {
        switch (comboBox_fields.SelectedIndex)
        {
            case 0:
                _presenter.UpdateDistrict(textBox_district.Text);
                break;

            case 1:
                _presenter.UpdateHousingDepartmentNumber((int)numericUpDown_housingDepartmentNumber.Value);
                break;

            case 2:
                _presenter.UpdateResidents(textBox_residentName.Text, textBox_residentHouseNum.Text);
                break;

            case 3:
                _presenter.UpdatePaidResidentsCount((int)numericUpDown_paidResidentsCount.Value);
                break;

            case 4:
                _presenter.UpdateTariff((double)numericUpDown_tariff.Value);
                break;

            case 5:
                _presenter.UpdateBalance(numericUpDown_balance.Value);
                break;

            case 6:
                _presenter.UpdateEmployeeCount((int)numericUpDown_employeeCount.Value);
                break;
        }

        label_saved_status.Visible = true;

        if (!_isShowedInfo)
        {
            _isShowedInfo = true;
            button_show.Text = "Скрыть информацию";
            label_show_info.Text = _latestDepartmentInfo;
        }
    }

    private void comboBox_fields_SelectedIndexChanged(object sender, EventArgs e)
    {
        textBox_district.Visible = false;
        numericUpDown_housingDepartmentNumber.Visible = false;
        textBox_residentHouseNum.Visible = false;
        textBox_residentName.Visible = false;
        numericUpDown_paidResidentsCount.Visible = false;
        numericUpDown_tariff.Visible = false;
        numericUpDown_balance.Visible = false;
        numericUpDown_employeeCount.Visible = false;

        button_save.Visible = true;

        switch (comboBox_fields.SelectedIndex)
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

    private void button_next_Click(object sender, EventArgs e)
    {
        panel2.Visible = true;
        tableLayoutPanel2.Visible = true;

        Size = new Size(MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);
        MaximumSize = new Size(MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);
        MinimumSize = new Size(MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);
    }

    private void button_show_Click(object sender, EventArgs e)
    {
        if (!_isShowedInfo)
        {
            _presenter.RefreshViews();
            label_show_info.Text = _latestDepartmentInfo;
            button_show.Text = "Скрыть информацию";
        }
        else
        {
            label_show_info.Text = string.Empty;
            button_show.Text = "Показать информацию";
        }

        _isShowedInfo = !_isShowedInfo;
    }

    private void button_save_Click(object sender, EventArgs e)
    {
        try
        {
            FillValues();
        }
        catch (Exception ex)
        {
            NativeMessageBox.MessageBox(
                IntPtr.Zero,
                ex.Message,
                "Ошибка",
                NativeMessageBox.MB_OK | NativeMessageBox.MB_ICONERROR
            );
        }
    }

    private void button_prev_Click(object sender, EventArgs e)
    {
        panel2.Visible = false;
        tableLayoutPanel2.Visible = false;

        Size = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
        MaximumSize = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
        MinimumSize = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
    }

    private void button_exit_Click(object sender, EventArgs e)
    {
        Close();
    }
}
