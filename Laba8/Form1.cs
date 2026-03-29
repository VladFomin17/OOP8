using System.Diagnostics.Contracts;
using System.DirectoryServices.ActiveDirectory;
using Laba1.service;
using Laba1.Src.Subject;
using Laba1.Src.util;

namespace Laba1;

/// <summary>
/// Главная форма приложения.
/// Отвечает за взаимодействие пользователя с системой управления ЖЭК.
/// </summary>
public partial class Form1 : Form
{
    /// <summary>Начальная высота окна.</summary>
    private const int START_WINDOW_HEIGHT = 451;

    /// <summary>Начальная ширина окна.</summary>
    private const int START_WINDOW_WIDTH = 526;

    /// <summary>Основная высота окна.</summary>
    private const int MAIN_WINDOW_HEIGHT = 723;

    /// <summary>Основная ширина окна.</summary>
    private const int MAIN_WINDOW_WIDTH = 628;

    /// <summary>
    /// Флаг отображения информации о департаменте.
    /// </summary>
    private static bool isShowedInfo = false;

    /// <summary>
    /// Посредник для работы с бизнес-логикой.
    /// </summary>
    private IHousingDepartmentPresenter _presenter;

    /// <summary>
    /// Инициализирует главную форму.
    /// </summary>
    public Form1()
    {
        InitializeComponent();
        Size = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
        MaximumSize = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
        MinimumSize = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
        panel2.Visible = false;
        tableLayoutPanel2.Visible = false;
        
        _presenter = new HousingDepartmentPresenter();
    }
    
    /// <summary>
    /// Сохраняет введённые пользователем данные в зависимости от выбранного поля.
    /// </summary>
    private void FillValues()
    {
        switch (comboBox_fields.SelectedIndex)
        {
            case 0:
                _presenter.UpdateDistrict(textBox_district.Text);
                break;
            case 1:
                _presenter.UpdateHousingDepartmentNumber(int.Parse(numericUpDown_housingDepartmentNumber.Text));
                break;
            case 2:
                _presenter.UpdateResidents(textBox_residentName.Text, textBox_residentHouseNum.Text);
                break;
            case 3:
                _presenter.UpdatePaidResidentsCount(int.Parse(numericUpDown_paidResidentsCount.Text));
                break;
            case 4:
                _presenter.UpdateTariff(double.Parse(numericUpDown_tariff.Text));
                break;
            case 5:
                _presenter.UpdateBalance(numericUpDown_balance.Value);
                break;
            case 6:
                _presenter.UpdateEmployeeCount(int.Parse(numericUpDown_employeeCount.Text));
                break;
        }
        
        label_saved_status.Visible = true;
        label_show_info.Text = _presenter.GetDepartmentInfo();
        if (!isShowedInfo)
        {
            button_show.Text = "Скрыть информацию";
            isShowedInfo = true;
        }
    }
    
    /// <summary>
    /// Обрабатывает изменение выбранного поля и отображает соответствующий ввод.
    /// </summary>
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

    /// <summary>
    /// Переход к основной форме (расширенный режим).
    /// </summary>
    private void button_next_Click(object sender, EventArgs e)
    {
        panel2.Visible = true;
        tableLayoutPanel2.Visible = true;
        Size = new Size(MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);
        MaximumSize = new Size(MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);
        MinimumSize = new Size(MAIN_WINDOW_WIDTH, MAIN_WINDOW_HEIGHT);
    }

    /// <summary>
    /// Показывает или скрывает информацию о департаменте.
    /// </summary>
    private void button_show_Click(object sender, EventArgs e)
    {
        if (!isShowedInfo)
        {
            label_show_info.Text = _presenter.GetDepartmentInfo();
            button_show.Text = "Скрыть информацию";
        }
        else
        {
            label_show_info.Text = "";
            button_show.Text = "Показать информацию";
        }
        
        isShowedInfo = !isShowedInfo;
    }

    /// <summary>
    /// Обработчик кнопки сохранения данных.
    /// </summary>
    private void button_save_Click(object sender, EventArgs e)
    {
        try
        {
            FillValues();
        }
        catch (Exception ex)
        {
            NativeMessageBox.MessageBox(
                0,
                ex.Message,
                "Ошибка",
                NativeMessageBox.MB_OK | NativeMessageBox.MB_ICONERROR
            );
        }
    }

    /// <summary>
    /// Возврат к начальному экрану.
    /// </summary>
    private void button_prev_Click(object sender, EventArgs e)
    {
        panel2.Visible = false;
        tableLayoutPanel2.Visible = false;
        Size = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
        MaximumSize = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
        MinimumSize = new Size(START_WINDOW_WIDTH, START_WINDOW_HEIGHT);
    }

    /// <summary>
    /// Закрывает приложение.
    /// </summary>
    private void button_exit_Click(object sender, EventArgs e)
    {
        Close();
    }
}