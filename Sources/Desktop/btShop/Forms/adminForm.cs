﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tuningAtelier.ENT;

namespace tuningAtelier.Forms
{
    public partial class adminForm : Form
    {
        #region Properties
        private Form enterForm;
        private int timerCounter = 0;
        private user userData = null;
        private user changingUser = null;
        private string userLogin = "";
        #endregion
        public adminForm(Form main, string login)
        {
            InitializeComponent();
            enterForm = main;
            userLogin = login;
        }
        #region Func
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            enterForm.Show();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            timerAFK.Start();
            using (BatteriesEntities db = new BatteriesEntities())
            {
                userData = db.user.Single(p => p.login == userLogin);
            }
            tabPageChangingUserData.Parent = null;
            setDataFunc();
            setComboBoxItems();
            loadUsersDataInDataGrid();
        }

        private void timerAFK_Tick(object sender, EventArgs e)
        {
            timerCounter++;
            if (timerCounter == 120)
            {
                this.Close();
                enterForm.Show();
            }
        }

        private void FormAdmin_VisibleChanged(object sender, EventArgs e)
        {
            timerCounter = 0;
            if (this.Visible == false)
            {
                timerAFK.Stop();
            }
            else
            {
                timerAFK.Start();
            }
        }

        private void tabPage_MouseMove(object sender, MouseEventArgs e)
        {
            timerCounter = 0;
        }

        private void tabPage_KeyDown(object sender, KeyEventArgs e)
        {
            timerCounter = 0;
        }

        private void loadUsersDataInDataGrid()
        {
            using (BatteriesEntities db = new BatteriesEntities())
            {
                dataGridViewUsers.DataSource = db.user
                    .Select(p => new
                    {
                        idUser = p.idUser,
                        login = p.login,
                        password = p.password,
                        gender = p.gender,
                        surname = p.surname,
                        name = p.name,
                        patronymic = p.patronymic,
                        userPhoto = p.userPhoto,
                        role = p.role,
                        birthday = p.birthday,
                        phoneNumber = p.phoneNumber,
                        status = p.status
                    }).ToList();

            }
        }

        private void loadSortedUsersDataInDataGrid()
        {
            using (BatteriesEntities db = new BatteriesEntities())
            {
                dataGridViewUsers.DataSource = db.user
                    .Select(p => new
                    {
                        idUser = p.idUser,
                        login = p.login,
                        password = p.password,
                        gender = p.gender,
                        surname = p.surname,
                        name = p.name,
                        patronymic = p.patronymic,
                        userPhoto = p.userPhoto,
                        role = p.role,
                        birthday = p.birthday,
                        phoneNumber = p.phoneNumber,
                        status = p.status
                    })
                    .Where(p => p.gender.Contains(comboBoxSortGender.Text) && p.role.Contains(comboBoxSortRole.Text) && p.status.Contains(comboBoxSortStatus.Text))
                    .Where(p => p.login.Contains(textBoxSearch.Text) || p.password.Contains(textBoxSearch.Text) || p.surname.Contains(textBoxSearch.Text)
                    || p.name.Contains(textBoxSearch.Text) || p.patronymic.Contains(textBoxSearch.Text)
                    || p.birthday.Contains(textBoxSearch.Text) || p.phoneNumber.Contains(textBoxSearch.Text))
                    .ToList();
            }
        }

        private bool checkUser()
        {
            using (BatteriesEntities db = new BatteriesEntities())
            {
                if (db.user.Any(p => p.login == textBoxAddNewUserLogin.Text))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void loadPhoto(PictureBox pictureBox)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox.Image = Image.FromFile(openFileDialog.FileName);
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void visibleChanged(bool vis)
        {
            foreach (var pb in tabPageProfile.Controls.OfType<TextBox>())
            {
                pb.Enabled = vis;
            }
            foreach (var pb in tabPageProfile.Controls.OfType<MaskedTextBox>())
            {
                pb.Enabled = vis;
            }
            foreach (var pb in tabPageProfile.Controls.OfType<RadioButton>())
            {
                pb.Enabled = vis;
            }
            foreach (var pb in tabPageProfile.Controls.OfType<ComboBox>())
            {
                pb.Enabled = vis;
            }
            buttonEdit.Enabled = !vis;
            buttonChangeImage.Enabled = vis;
            buttonAccept.Enabled = vis;
        }

        private void changeVisibleForChangingUserData(bool vis)
        {
            foreach (var pb in tabPageChangingUserData.Controls.OfType<TextBox>())
            {
                pb.Enabled = vis;
            }
            foreach (var pb in tabPageChangingUserData.Controls.OfType<MaskedTextBox>())
            {
                pb.Enabled = vis;
            }
            foreach (var pb in tabPageChangingUserData.Controls.OfType<RadioButton>())
            {
                pb.Enabled = vis;
            }
            foreach (var pb in tabPageChangingUserData.Controls.OfType<ComboBox>())
            {
                pb.Enabled = vis;
            }
            buttonChangeUserAccept.Enabled = vis;
            buttonChangeUserPhoto.Enabled = vis;
        }

        private void updateData()
        {
            string rbText = "";
            foreach (var pb in tabPageProfile.Controls.OfType<RadioButton>())
            {
                if (pb.Checked)
                {
                    rbText = pb.Text;
                }
            }
            userData.login = textBoxLogin.Text;
            userData.password = textBoxPassword.Text;
            userData.name = textBoxName.Text;
            userData.surname = textBoxSurname.Text;
            userData.patronymic = textBoxPatronymic.Text;
            userData.role = comboBoxRole.Text;
            userData.birthday = maskedTextBoxBirthday.Text;
            userData.phoneNumber = maskedTextBoxPhoneNumber.Text;
            userData.gender = rbText;
            if (pictureBoxProfileImage.Image != null)
            {
                userData.userPhoto = convertImageToBytes(pictureBoxProfileImage.Image);
            }
        }

        private void updateDataSelectedUser()
        {
            string rbText = "";
            foreach (var pb in tabPageChangingUserData.Controls.OfType<RadioButton>())
            {
                if (pb.Checked)
                {
                    rbText = pb.Text;
                }
            }
            changingUser.login = textBoxChangeUserLogin.Text;
            changingUser.password = textBoxChangeUserPassword.Text;
            changingUser.name = textBoxChangeUserName.Text;
            changingUser.surname = textBoxChangeUserSurname.Text;
            changingUser.patronymic = textBoxChangeUserPatronymic.Text;
            changingUser.role = comboBoxChangeUserRole.Text;
            changingUser.birthday = maskedTextBoxChangeUserBirthday.Text;
            changingUser.phoneNumber = maskedTextBoxChangeUserPhoneNumber.Text;
            changingUser.status = comboBoxChangeUserStatus.Text;
            changingUser.gender = rbText;
            if (pictureBoxChangeUserPhoto.Image != null)
            {
                changingUser.userPhoto = convertImageToBytes(pictureBoxChangeUserPhoto.Image);
            }
        }


        private void setDataFunc()
        {
            textBoxLogin.Text = userData.login;
            textBoxPassword.Text = userData.password;
            textBoxName.Text = userData.name;
            textBoxSurname.Text = userData.surname;
            textBoxPatronymic.Text = userData.patronymic;
            maskedTextBoxBirthday.Text = userData.birthday;
            maskedTextBoxPhoneNumber.Text = userData.phoneNumber;
            if (userData.userPhoto != null)
            {
                pictureBoxProfileImage.Image = convertByteArrayToImage(userData.userPhoto);
            }
            if (userData.gender == "Мужской")
            {
                radioButtonMale.Checked = true;
            }
            else
            {
                radioButtonFemale.Checked = true;
            }
        }

        private void setDataFuncForChangeUserData(user userData)
        {
            textBoxChangeUserLogin.Text = userData.login;
            textBoxChangeUserPassword.Text = userData.password;
            textBoxChangeUserName.Text = userData.name;
            textBoxChangeUserSurname.Text = userData.surname;
            textBoxChangeUserPatronymic.Text = userData.patronymic;
            maskedTextBoxChangeUserBirthday.Text = userData.birthday;
            maskedTextBoxChangeUserPhoneNumber.Text = userData.phoneNumber;
            comboBoxChangeUserRole.Text = userData.role;
            comboBoxChangeUserStatus.Text = userData.status;
            if (userData.userPhoto != null)
            {
                pictureBoxChangeUserPhoto.Image = convertByteArrayToImage(userData.userPhoto);
            }
            if (userData.gender == "Мужской")
            {
                radioButtonChangeUserMale.Checked = true;
            }
            else
            {
                radioButtonChangeUserFemale.Checked = true;
            }
        }

        private void setComboBoxItems()
        {
            using (BatteriesEntities db = new BatteriesEntities())
            {
                foreach (var userRole in db.userRoles.ToList())
                {
                    comboBoxAddNewUserRole.Items.Add(userRole.role);
                    comboBoxRole.Items.Add(userRole.role);
                    comboBoxSortRole.Items.Add(userRole.role);
                    comboBoxChangeUserRole.Items.Add(userRole.role);
                }
                foreach (var userGender in db.userGenders.ToList())
                {
                    comboBoxSortGender.Items.Add(userGender.userGender);
                }
                foreach (var userStatus in db.userStatus.ToList())
                {
                    comboBoxSortStatus.Items.Add(userStatus.status);
                    comboBoxChangeUserStatus.Items.Add(userStatus.status);
                }
                comboBoxRole.Text = userData.role;
                comboBoxAddNewUserRole.Text = "Пользователь";
            }
        }

        private Image convertByteArrayToImage(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                return Image.FromStream(memoryStream);
            }
        }

        private byte[] convertImageToBytes(Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
        #endregion


        #region ChangeProfileProperties
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            visibleChanged(true);
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            visibleChanged(false);
            using (BatteriesEntities db = new BatteriesEntities())
            {
                updateData();
                db.Entry(userData).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            MessageBox.Show("Успешное изменение данных", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonChangeImage_Click(object sender, EventArgs e)
        {
            loadPhoto(pictureBoxProfileImage);
        }
        #endregion


        #region AddNewUser
        private void buttonAddNewUserRegister_Click(object sender, EventArgs e)
        {
            foreach (var pb in tabPageAddNew.Controls.OfType<TextBox>())
            {
                if (pb.Text == "" && pb.Name != "textBoxPatronymic")
                {
                    MessageBox.Show("Одно или несколько полей пустые", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            foreach (var pb in tabPageAddNew.Controls.OfType<MaskedTextBox>())
            {
                if (!pb.MaskCompleted)
                {
                    MessageBox.Show("Одно или несколько полей пустые", "Ошибка");
                    return;
                }
            }
            if (textBoxAddNewUserPassword.Text == textBoxAddNewUserAcceptPassword.Text)
            {
                if (checkUser())
                {
                    MessageBox.Show("Такой пользователь уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (radioButtonAddNewUserFemale.Checked || radioButtonAddNewUserMale.Checked)
                {
                    string rbText = "";
                    foreach (var pb in tabPageAddNew.Controls.OfType<RadioButton>())
                    {
                        if (pb.Checked)
                        {
                            rbText = pb.Text;
                        }
                    }
                    if (pictureBoxAddNewUserPhoto.Image == null)
                    {
                        using (BatteriesEntities db = new BatteriesEntities())
                        {
                            db.user.Add(new user
                            {
                                login = textBoxAddNewUserLogin.Text.Trim(),
                                password = textBoxAddNewUserPassword.Text.Trim(),
                                gender = rbText,
                                surname = textBoxAddNewUserSurname.Text.Trim(),
                                name = textBoxAddNewUserName.Text.Trim(),
                                patronymic = textBoxAddNewUserPatronymic.Text.Trim(),
                                role = comboBoxAddNewUserRole.Text,
                                birthday = maskedTextBoxAddNewUserBirthday.Text,
                                phoneNumber = maskedTextBoxAddNewUserPhoneNumber.Text,
                                status = "Активен"
                            });
                            db.SaveChanges();
                        }
                        MessageBox.Show("Новый пользователь создан", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        using (BatteriesEntities db = new BatteriesEntities())
                        {
                            db.user.Add(new user
                            {
                                login = textBoxAddNewUserLogin.Text.Trim(),
                                password = textBoxAddNewUserPassword.Text.Trim(),
                                gender = rbText,
                                surname = textBoxAddNewUserSurname.Text.Trim(),
                                name = textBoxAddNewUserName.Text.Trim(),
                                patronymic = textBoxAddNewUserPatronymic.Text.Trim(),
                                userPhoto = convertImageToBytes(pictureBoxAddNewUserPhoto.Image),
                                role = comboBoxAddNewUserRole.Text,
                                birthday = maskedTextBoxAddNewUserBirthday.Text,
                                phoneNumber = maskedTextBoxAddNewUserPhoneNumber.Text,
                                status = "Активен"
                            });
                            db.SaveChanges();
                        }
                        MessageBox.Show("Новый пользователь создан", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали пол", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddNewUserLoadPhoto_Click(object sender, EventArgs e)
        {
            loadPhoto(pictureBoxAddNewUserPhoto);
        }
        #endregion


        #region WatchAllUsers
        private void buttonSort_Click(object sender, EventArgs e)
        {
            loadSortedUsersDataInDataGrid();
        }

        private void dataGridViewUsers_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridViewUsers.CurrentRow.Index != -1)
            {
                int userId = Convert.ToInt32(dataGridViewUsers.CurrentRow.Cells["idUser"].Value);
                using (BatteriesEntities db = new BatteriesEntities())
                {
                    changingUser = db.user.Single(p => p.idUser == userId);
                    setDataFuncForChangeUserData(changingUser);
                }
                changeVisibleForChangingUserData(true);
                tabPageChangingUserData.Parent = tabPage;
                tabPage.SelectedTab = tabPageChangingUserData;
            }
        }

        private void buttonRefreshDGV_Click(object sender, EventArgs e)
        {
            loadUsersDataInDataGrid();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            loadSortedUsersDataInDataGrid();
        }
        #endregion


        #region ChangeSelectedUserProperties
        private void buttonChangeUserPhoto_Click(object sender, EventArgs e)
        {
            loadPhoto(pictureBoxChangeUserPhoto);
        }

        private void buttonChangeUserAccept_Click(object sender, EventArgs e)
        {
            changeVisibleForChangingUserData(false);
            using (BatteriesEntities db = new BatteriesEntities())
            {
                updateDataSelectedUser();
                db.Entry(changingUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            MessageBox.Show("Успешное изменение данных", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tabPage.SelectedTab = tabPageAllUserData;
            tabPageChangingUserData.Parent = null;
        }
        #endregion
    }
}
