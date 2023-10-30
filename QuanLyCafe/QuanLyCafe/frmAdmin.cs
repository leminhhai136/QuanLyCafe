using Microsoft.Office.Interop.Excel;
using QuanLyCafe.DAO;
using QuanLyCafe.DTO;
using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace QuanLyCafe
{
    public partial class fAdmin : Form
    {
        string macu1;
        string macu2;
        string macu3;
        string tencu;
        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadListCategory();
            LoadListTable();
            LoadListAccount();
            LoadListStaff();
            LoadListSupplier();
            LoadListCustom();
            LoadCategoryIntoCombobox(cbFoodCategory);
            LoadPositionIntoComboBox(cbPositionStaff);
            LoadTableIntoComboBox(cbCustomTable);
        }

        #region methods

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);
            return listFood;
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
            lbDT.Text = "Danh sách có " + (dtgvBill.RowCount-1) + " hoá đơn";
        }
        #endregion

        
        void LoadListAccount()
        {
            dtgvAccount.DataSource = AccountDAO.Instance.GetListAccount();
            lbTK.Text = "Danh sách có " + (dtgvAccount.RowCount-1) + " tài khoản";
        }
        void LoadListFood()
        {
            dtgvFood.DataSource = FoodDAO.Instance.GetListFood();
            lbTA.Text = "Danh sách có " + dtgvFood.RowCount + " món ăn";
        }
        void LoadListCategory()
        {
            dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategory();
            lbDM.Text = "Danh sách có " + dtgvCategory.RowCount + " danh mục";
        }
        void LoadListTable()
        {
            dtgvTable.DataSource = TableDAO.Instance.LoadTableList();
            lbBA.Text = "Danh sách có " + dtgvTable.RowCount + " bàn ăn";
        }
        void LoadListStaff()
        {
            dtgvStaff.DataSource = StaffDAO.Instance.GetListStaff();
            lbNV.Text = "Danh sách có " + dtgvStaff.RowCount + " nhân viên";
        }
        void LoadListCustom()
        {
            dtgvCustom.DataSource = CustomDAO.Instance.GetListCustom();
            lbKH.Text = "Danh sách có " + dtgvCustom.RowCount + " khách hàng";
        }
        void LoadListSupplier()
        {
            dtgvSupplier.DataSource = SupplierDAO.Instance.GetListSupplier();
            lbNCC.Text = "Danh sách có " + dtgvSupplier.RowCount + " nhà cung cấp";
        }
        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadPositionIntoComboBox(ComboBox cb)
        {
            cb.DataSource = PositionDAO.Instance.GetListPosition();
            cb.DisplayMember = "Name";
        }
        void LoadTableIntoComboBox(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }
        List<Category> SearchCategoryByName(string name)
        {
            List<Category> listCategory = 
            CategoryDAO.Instance.SearchCategoryByName(name);
            return listCategory;
        }
        List<Table> SearchTableByName(string name)
        {
            List<Table> listTable =
            TableDAO.Instance.SearchTableByName(name);
            return listTable;
        }
        List<Staff> SearchStaffByName(string name)
        {
            List<Staff> ListStaff =
            StaffDAO.Instance.SearchStaffByName(name);
            return ListStaff;
        }
        List<Custom> SearchCustomByName(string name)
        {
            List<Custom> listCustom =
            CustomDAO.Instance.SearchCustomByName(name);
            return listCustom;
        }
        List<Supplier> SearchSupplierByName(string )
        {
            List<Supplier> listSupplier = 
            SupplierDAO.Instance.SearchSupplierByName(Sdt);
            return listSupplier;
        }
        List<Account> SearchAccountByName(string name)
        {
            List<Account> ListAccount =
            AccountDAO.Instance.SearchAccountByName(name);
            return ListAccount;
        }

        #region events
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            lbDT.Text = "Danh sách có " + (dtgvBill.RowCount - 1) + " hoá đơn";
        }
        #endregion

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                cbFoodCategory.SelectedItem = cateogory;

                int index = -1;
                int i = 0;
                foreach (Category item in cbFoodCategory.Items)
                {
                    if (item.ID == cateogory.ID)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }

                cbFoodCategory.SelectedIndex = index;
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (txbFoodName.Text == "")
            {
                MessageBox.Show("Hãy nhập tên món");
                return;
            }
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            
           
            
            if (MessageBox.Show("Bạn có muốn thêm thức ăn ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (FoodDAO.Instance.InsertFood(name, categoryID, price))
                {
                    MessageBox.Show("Thêm món thành công");
                    LoadListFood();
                    if (insertFood != null)
                        insertFood(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm thức ăn");
                }
            }
                
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            if (txbFoodName.Text == "")
            {
                MessageBox.Show("Hãy nhập tên món");
                return;
            }
            if (txbFoodID.Text == "")
            {
                MessageBox.Show("Hãy chọn món cần sửa ");
                return;
            }
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodID.Text);
            if (name != tencu)
            {
                if (FoodDAO.Kttrungten(name))
                {
                    MessageBox.Show("Bạn nhập nhà món đã tồn tại.", "Thông Báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (MessageBox.Show("Bạn có muốn thay đổi thức ăn ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
                {
                    MessageBox.Show("Sửa món thành công");
                    LoadListFood();
                    if (updateFood != null)
                        updateFood(this, new EventArgs());
                    FoodDAO.Instance.GetListFood();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi sửa thức ăn");
                }
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            if (txbFoodName.Text == "")
            {
                MessageBox.Show("Hãy chọn món cần xoá trước");
                return ;
            }
            if (txbFoodID.Text == "")
            {
                MessageBox.Show("Hãy chọn món cần xoá ");
                return;
            }
            int id = Convert.ToInt32(txbFoodID.Text);

            if (MessageBox.Show("Bạn có muốn xoá thức ăn ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (FoodDAO.Instance.DeleteFood(id))
                {
                    MessageBox.Show("Xóa món thành công");
                    LoadListFood();
                    if (deleteFood != null)
                        deleteFood(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa thức ăn");
                }
            }
        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        private void dtgvFood_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dtgvFood.RowCount <= 0) return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvFood.Rows[e.RowIndex];
                txbFoodID.Text = row.Cells[3].Value.ToString();
                txbFoodName.Text = row.Cells[2].Value.ToString();
                nmFoodPrice.Text = row.Cells[0].Value.ToString();
                tencu = txbFoodName.Text;
            }
        }

        private void dtgvCategory_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dtgvCategory.RowCount <= 0) return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvCategory.Rows[e.RowIndex];
                txbCategoryID.Text = row.Cells[1].Value.ToString();
                txbCategoryName.Text = row.Cells[0].Value.ToString();
                tencu = txbCategoryName.Text;
            }
        }

        private void dtgvTable_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dtgvTable.RowCount <= 0) return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvTable.Rows[e.RowIndex];
                txbTableID.Text = row.Cells[2].Value.ToString();
                txbTableName.Text = row.Cells[1].Value.ToString();
                cbTableStatus.Text = row.Cells[0].Value.ToString();
                tencu = txbTableName.Text;
            }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            dtgvFood.DataSource = SearchFoodByName(txbSearchFoodName.Text);
            lbTA.Text = "Danh sách có " + dtgvFood.RowCount + " món ăn";
        }

        private void dtgvAccount_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dtgvAccount.RowCount <= 0) return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvAccount.Rows[e.RowIndex];
                txbUserName.Text = row.Cells[0].Value.ToString();
                txbDisplayName.Text = row.Cells[1].Value.ToString();
                tencu = txbUserName.Text;
            }
        }
        void AddAccount(string userName, string displayName, int type)
        {
            string name = txbUserName.Text;
            if (txbUserName.Text == "" || txbDisplayName.Text == "")
            {
                MessageBox.Show("Chưa nhập tài khoản hoặc tên !!!");
                return;
            }
            
                if (AccountDAO.Kttrungten(name))
                {
                    MessageBox.Show("Tài khoản đã được sử dụng", "Thông Báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
          
            if (MessageBox.Show("Bạn có muốn thêm tài khoản ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
                {
                    MessageBox.Show("Thêm tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại");
                }

                LoadListAccount();
            }
        }

        void EditAccount(string userName, string displayName, int type)
        {
            string name = txbUserName.Text;
            if (txbUserName.Text == "" || txbDisplayName.Text == "")
            {
                MessageBox.Show("Chưa nhập tài khoản hoặc tên !!!");
                return;
            }
            if (name != tencu)
            {
                if (SupplierDAO.Kttrungten(name))
                {
                    MessageBox.Show("Bạn nhập tài khoản đã tồn tại.", "Thông Báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (MessageBox.Show("Bạn có muốn cập nhật tài khoản ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (AccountDAO.Instance.UpdateAccount(userName, displayName, type, tencu))
                {
                    MessageBox.Show("Cập nhật tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại");
                }

                LoadListAccount();
            }
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Xoá rồi đăng nhập kiểu gì ?");
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá tài khoản ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (AccountDAO.Instance.DeleteAccount(userName))
                {
                    MessageBox.Show("Xóa tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Xóa tài khoản thất bại");
                }

                LoadListAccount();
            }
        }
        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            AddAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            EditAccount(userName, displayName, type);
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadListAccount();
        }

        private void btnResetPassWord_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            ResetPass(userName);
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            if (txbCategoryName.Text == "")
            {
                MessageBox.Show("Hãy nhập tên danh mục");
                return;
            }
           
            if (CategoryDAO.Kttrungten(name))
            {
                MessageBox.Show("Bạn nhập danh mục đã tồn tại.", "Thông Báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
  
            if (MessageBox.Show("Bạn có muốn thêm danh mục ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (CategoryDAO.Instance.InsertCategory(name))
                {
                    MessageBox.Show("Thêm danh mục thành công");
                    LoadListCategory();
                    LoadListFood();
                    LoadCategoryIntoCombobox(cbFoodCategory);
                    if (insertCategory != null)
                        insertCategory(this, new EventArgs());
                    CategoryDAO.Instance.GetListCategory();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm danh mục");
                }
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            if (txbCategoryName.Text == "")
            {
                MessageBox.Show("Hãy chọn danh mục cần xoá");
                return;
            }
            int id = Convert.ToInt32(txbCategoryID.Text);
            if (MessageBox.Show("Bạn có muốn xoá danh mục ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (CategoryDAO.Instance.DeleteCategory(id))
                {
                    MessageBox.Show("Xoá danh mục thành công");
                    LoadListCategory();
                    LoadListFood();
                    LoadCategoryIntoCombobox(cbFoodCategory);
                    if (deleteCategory != null)
                        deleteCategory(this, new EventArgs());
                    CategoryDAO.Instance.GetListCategory();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xoá danh mục");
                }
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            if (txbCategoryName.Text == "")
            {
                MessageBox.Show("Hãy nhập tên danh mục");
                return;
            }
            string name = txbCategoryName.Text;
            int id = Convert.ToInt32(txbCategoryID.Text);
            if (name != tencu)
            {
                if (CategoryDAO.Kttrungten(name))
                {
                    MessageBox.Show("Bạn nhập danh mục đã tồn tại.", "Thông Báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (MessageBox.Show("Bạn có muốn sửa danh mục ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (CategoryDAO.Instance.UpdateCategory(name, id))
                {
                    MessageBox.Show("Sửa danh mục thành công");
                    LoadListCategory();
                    LoadListFood();
                    LoadCategoryIntoCombobox(cbFoodCategory);
                    if (updateCategory != null)
                        updateCategory(this, new EventArgs());
                    CategoryDAO.Instance.GetListCategory();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi sửa danh mục");
                }
            }
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }
        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }

        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }

        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }
        void LoadStatusTable(ComboBox cb)
        {
            cbTableStatus.DataSource = TableDAO.Instance.LoadStatusTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            string status = cbTableStatus.Text;
            if (txbTableName.Text == "")
            {
                MessageBox.Show("Hãy nhập tên bàn");
                return;
            }
            if (cbTableStatus.Text == "")
            {
                MessageBox.Show("Hãy chọn trạng thái bàn");
                return;
            }
            
                if (TableDAO.Kttrungten(name))
                {
                    MessageBox.Show("Bạn nhập tên bàn đã tồn tại", "Thông Báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
          
            if (MessageBox.Show("Bạn có muốn thêm bàn ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (TableDAO.Instance.InsertTable(name, status))
                {
                    MessageBox.Show("Thêm bàn thành công");
                    LoadListTable();
                    if (insertTable != null)
                        insertTable(this, new EventArgs());
                    TableDAO.Instance.LoadTableList();
                    LoadTableIntoComboBox(cbCustomTable);
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm bàn");
                }
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            if (txbTableID.Text == "")
            {
                MessageBox.Show("Hãy chọn bàn cần xoá");
                return;
            }
            int id = Convert.ToInt32(txbTableID.Text);
            if (MessageBox.Show("Bạn có muốn xoá bàn ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (TableDAO.Instance.DeleteTable(id))
                {
                    MessageBox.Show("Xoá bàn thành công");
                    LoadListTable();
                    if (deleteTable != null)
                        deleteTable(this, new EventArgs());
                    TableDAO.Instance.LoadTableList();
                    LoadTableIntoComboBox(cbCustomTable);
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xoá bàn");
                }
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            string status = cbTableStatus.Text;
            if (txbTableName.Text == "")
            {
                MessageBox.Show("Hãy nhập tên bàn");
                return;
            }
            if (cbTableStatus.Text == "")
            {
                MessageBox.Show("Hãy chọn trạng thái bàn");
                return;
            }
            if (txbTableID.Text == "")
            {
                MessageBox.Show("Hãy chọn bàn cần sửa");
                return;
            }
            if (name != tencu)
            {
                if (TableDAO.Kttrungten(name))
                {
                    MessageBox.Show("Bạn nhập tên bàn đã tồn tại.", "Thông Báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            int id = Convert.ToInt32(txbTableID.Text);
            if (MessageBox.Show("Bạn có muốn thay đổi bàn ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (TableDAO.Instance.UpdateTable(id ,name, status))
                {
                    MessageBox.Show("Thay đổi thành công");
                    LoadListTable();
                    if (updateTable != null)
                        updateTable(this, new EventArgs());
                    TableDAO.Instance.LoadTableList();
                    LoadTableIntoComboBox(cbCustomTable);
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thay đổi bàn");
                }
            }
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }
        private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }

        private event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }

        private event EventHandler updateTable;
        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnXuatExcelFood_Click(object sender, EventArgs e)
        {
            if (dtgvFood.RowCount <= 0) return;
            Public1.XuatFileExcel(dtgvFood, "food.xlsx");
        }

        private void btnXuatExcelCate_Click(object sender, EventArgs e)
        {
            if (dtgvCategory.RowCount <= 0) return;
            Public1.XuatFileExcel(dtgvCategory, "danhmuc.xlsx");
        }

        private void btnXuatExcelTable_Click(object sender, EventArgs e)
        {
            if (dtgvTable.RowCount <= 0) return;
            Public1.XuatFileExcel(dtgvTable, "danhsachban.xlsx");
        }

        private void btnXuatExcelTK_Click(object sender, EventArgs e)
        {
            if (dtgvAccount.RowCount <= 0) return;
            Public.XuatFileExcel(dtgvAccount, "danhsachtaikhoan.xlsx");
        }


        private void btnShowStaff_Click(object sender, EventArgs e)
        {
            LoadListStaff();
        }

        private void btnShowCustom_Click(object sender, EventArgs e)
        {
            LoadListCustom();
        }

        private void btnShowSupplier_Click(object sender, EventArgs e)
        {
            LoadListSupplier();
        }

        private void dtgvStaff_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dtgvStaff.RowCount <= 0) return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvStaff.Rows[e.RowIndex];
                txbStaffName.Text = row.Cells[0].Value.ToString();
                txbStaffID.Text = row.Cells[1].Value.ToString();
                txbStaffBirth.Text = row.Cells[2].Value.ToString();
                txbStaffSDT.Text = row.Cells[3].Value.ToString();
                cbPositionStaff.Text = row.Cells["Position"].Value.ToString();
                macu1 = txbStaffID.Text;
            }
        }

        private void dtgvCustom_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dtgvCustom.RowCount <= 0) return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvCustom.Rows[e.RowIndex];
                txbCustomName.Text = row.Cells[0].Value.ToString();
                txbCustomID.Text = row.Cells[1].Value.ToString();
                cbCustomTable.Text = row.Cells[2].Value.ToString();
                txbCustomSDT.Text = row.Cells[3].Value.ToString();
                macu2 = txbCustomID.Text;
            }
        }

        private void dtgvSupplier_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dtgvSupplier.RowCount <= 0) return;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvSupplier.Rows[e.RowIndex];
                txbSupplierName.Text = row.Cells[0].Value.ToString();
                txbSupplierID.Text = row.Cells[1].Value.ToString();
                txbSupplierSDT.Text = row.Cells[2].Value.ToString();
                macu3 = txbSupplierID.Text;
                tencu = txbSupplierName.Text;
            }
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            string name = txbStaffName.Text;
            string id = txbStaffID.Text;
            string birth = txbStaffBirth.Text;
            string sdt = txbStaffSDT.Text;
            string position = cbPositionStaff.Text;
            if (txbStaffName.Text == ""||txbStaffID.Text=="" || txbStaffBirth.Text == "" || txbStaffSDT.Text == "" || cbPositionStaff.Text == "")
            {
                MessageBox.Show("Không được để trống");
                return;
            }
            if (StaffDAO.Kttrungma(id))
            {
                MessageBox.Show("Bạn nhập mã đã tồn tại.", "Thông Báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Bạn có muốn thêm nhân viên ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (StaffDAO.Instance.AddStaff(name, id, birth, sdt, position))
                {
                    MessageBox.Show("Thêm nhân viên thành công");
                    LoadListStaff();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm nhân viên");
                }
            }
        }

        private void btnDeleteStaff_Click(object sender, EventArgs e)
        {

            string name = txbStaffName.Text;
            string id = txbStaffID.Text;
            string birth = txbStaffBirth.Text;
            string sdt = txbStaffSDT.Text;
            string position = cbPositionStaff.Text;
            if (txbStaffName.Text == "" || txbStaffID.Text == "" || txbStaffBirth.Text == "" || txbStaffSDT.Text == "" || cbPositionStaff.Text == "")
            {
                MessageBox.Show("Chọn nhân viên cần xoá");
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá nhân viên ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (StaffDAO.Instance.DeleteStaff(id))
                {
                    MessageBox.Show("Xoá nhân viên thành công");
                    LoadListStaff();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xoá nhân viên");
                }
            }
        }

        private void btnUpdateStaff_Click(object sender, EventArgs e)
        {
            string name = txbStaffName.Text;
            string id = txbStaffID.Text;
            string birth = txbStaffBirth.Text;
            string sdt = txbStaffSDT.Text;
            string position = cbPositionStaff.Text;
            if (txbStaffName.Text == "" || txbStaffID.Text == "" || txbStaffBirth.Text == "" || txbStaffSDT.Text == "" || cbPositionStaff.Text == "")
            {
                MessageBox.Show("Không được để trống");
                return;
            }
            if (id != macu1)
            {
                if (StaffDAO.Kttrungma(id))
                {
                    MessageBox.Show("Bạn nhập mã đã tồn tại.", "Thông Báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
            if (MessageBox.Show("Bạn có muốn sửa thông tin nhân viên ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (StaffDAO.Instance.UpdateStaff(name, id, birth, sdt, position, macu1))
                {
                    MessageBox.Show("Sửa thông tin nhân viên thành công");
                    LoadListStaff();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi Sửa thông tin nhân viên");
                }
            }
        }

        private void btnAddCustom_Click(object sender, EventArgs e)
        {
            string name = txbCustomName.Text;
            string id = txbCustomID.Text;
            string sdt = txbCustomSDT.Text;
            string table = cbCustomTable.Text;
            if (name == "" || id == "" || sdt == "" || table == "")
            {
                MessageBox.Show("Không được để trống");
                return;
            }
            if (CustomDAO.Kttrungma(id))
            {
                MessageBox.Show("Bạn nhập mã đã tồn tại.", "Thông Báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Bạn có muốn thêm khách đặt bàn ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (CustomDAO.Instance.AddCustom(name, id, sdt, table))
                {
                    MessageBox.Show("Thêm khách hàng thành công");
                    LoadListCustom();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm khách hàng");
                }
            }
        }

        private void btnDeleteCustom_Click(object sender, EventArgs e)
        {
            string name = txbCustomName.Text;
            string id = txbCustomID.Text;
            string sdt = txbCustomSDT.Text;
            string table = cbCustomTable.Text;
            if (name == "" || id == "" || sdt == "" || table == "")
            {
                MessageBox.Show("Hãy chọn khách hàng");
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá khách hàng ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (CustomDAO.Instance.DeleteCustom(id))
                {
                    MessageBox.Show("Xoá khách hàng thành công");
                    LoadListCustom();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xoá khách hàng");
                }
            }
        }

        private void btnUpdateCustom_Click(object sender, EventArgs e)
        {
            string name = txbCustomName.Text;
            string id = txbCustomID.Text;
            string sdt = txbCustomSDT.Text;
            string table = cbCustomTable.Text;
            if (name == "" || id == "" || sdt == "" || table == "")
            {
                MessageBox.Show("Không được để trống");
                return;
            }
            if (id != macu2)
            {
                if (CustomDAO.Kttrungma(id))
                {
                    MessageBox.Show("Bạn nhập mã đã tồn tại.", "Thông Báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (MessageBox.Show("Bạn có muốn sửa thông tin khách đặt bàn ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (CustomDAO.Instance.UpdateCustom(name, id, sdt, table, macu2))
                {
                    MessageBox.Show("Sửa thông tin khách hàng thành công");
                    LoadListCustom();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi sửa thông tin khách hàng");
                }
            }
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            string name = txbSupplierName.Text;
            string id = txbSupplierID.Text;
            string sdt = txbSupplierSDT.Text;
            if (name == "" || id == "" || sdt == "")
            {
                MessageBox.Show("Không được để trống");
                return;
            }
            if (SupplierDAO.Kttrungma(id))
            {
                MessageBox.Show("Bạn nhập mã đã tồn tại.", "Thông Báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (SupplierDAO.Kttrungten(name))
            {
                MessageBox.Show("Bạn nhập nhà cung cấp đã tồn tại.", "Thông Báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("Bạn có muốn thêm thêm nhà cung cấp ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (SupplierDAO.Instance.AddSupplier(name, id, sdt))
                {
                    MessageBox.Show("Thêm nhà cung cấp thành công");
                    LoadListSupplier();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm nhà cung cấp");
                }
            }
        }

        private void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            string name = txbSupplierName.Text;
            string id = txbSupplierID.Text;
            string sdt = txbSupplierSDT.Text;
            if (name == "" || id == "" || sdt == "")
            {
                MessageBox.Show("Hãy chọn nhà cung cấp cần xoá");
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá nhà cung cấp ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (SupplierDAO.Instance.DeleteSupplier(id))
                {
                    MessageBox.Show("Xoá nhà cung cấp thành công");
                    LoadListSupplier();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xoá nhà cung cấp");
                }
            }
        }

        private void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            string name = txbSupplierName.Text;
            string id = txbSupplierID.Text;
            string sdt = txbSupplierSDT.Text;
            if (name == "" || id == "" || sdt == "")
            {
                MessageBox.Show("Không được để trống");
                return;
            }
            if (id != macu3)
            {
                if (CustomDAO.Kttrungma(id))
                {
                    MessageBox.Show("Bạn nhập mã đã tồn tại.", "Thông Báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if(name != tencu)
            {
                if (SupplierDAO.Kttrungten(name))
                {
                    MessageBox.Show("Bạn nhập nhà cung cấp đã tồn tại.", "Thông Báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (MessageBox.Show("Bạn có muốn sửa thông tin nhà cung cấp ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (SupplierDAO.Instance.UpdateSupplier(name, id, sdt, macu3))
                {
                    MessageBox.Show("Sửa thông tin nhà cung cấp thành công");
                    LoadListSupplier();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi sửa thông tin nhà cung cấp");
                }
            }
        }

        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            dtgvCategory.DataSource = SearchCategoryByName(txbSearchCategoryName.Text);
            lbDM.Text = "Danh sách có " + dtgvCategory.RowCount + " danh mục";
        }

        private void btnSearchTable_Click(object sender, EventArgs e)
        {
            dtgvTable.DataSource = SearchTableByName(txbSearchTableName.Text);
            lbBA.Text = "Danh sách có " + dtgvTable.RowCount + " bàn ăn";
        }

        private void btnSearchStaff_Click(object sender, EventArgs e)
        {
            dtgvStaff.DataSource = SearchStaffByName(txbSeachStaffName.Text);
            lbNV.Text = "Danh sách có " + dtgvStaff.RowCount + " nhân viên";
        }

        private void btnSearchCustom_Click(object sender, EventArgs e)
        {
            dtgvCustom.DataSource = SearchCustomByName(txbSearchCustomName.Text);
            lbKH.Text = "Danh sách có " + dtgvCustom.RowCount + " khách hàng";
        }

        private void btnSearchSupplier_Click(object sender, EventArgs e)
        {
            dtgvSupplier.DataSource = SearchSupplierByName(txbSearchSupplierName.Text);
            lbNCC.Text = "Danh sách có " + dtgvSupplier.RowCount + " nhà cung cấp";
        }
        

        private void btnXuatExcelStaff_Click(object sender, EventArgs e)
        {
            if (dtgvStaff.RowCount <= 0) return;
            Public1.XuatFileExcel(dtgvStaff, "danhsachnhanvien.xlsx");
        }

        private void btnXuatExcelCustom_Click(object sender, EventArgs e)
        {
            if (dtgvCustom.RowCount <= 0) return;
            Public1.XuatFileExcel(dtgvCustom, "danhsachkhachhang.xlsx");
        }

        private void btnXuatExcelSupplier_Click(object sender, EventArgs e)
        {
            if (dtgvSupplier.RowCount <= 0) return;
            Public1.XuatFileExcel(dtgvSupplier, "danhsachnhacungcap.xlsx");
        }

        private void btnQuit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuit2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuit3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuit4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuit5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuit6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuit7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXuatExcelBill_Click(object sender, EventArgs e)
        {
            if (dtgvBill.RowCount <= 0) return;
            Public.XuatFileExcel(dtgvBill, "doanhthu.xlsx");
        }

        private void txbCustomSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            
        }

        

        private void txbSupplierSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            
        }


        private void txbStaffSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txbSearchFoodName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txbSearchCategoryName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtpkFromDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel23_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel26_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tpFoodCategory_Click(object sender, EventArgs e)
        {

        }

        private void txbDisplayName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbSearchTableName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbSearchSupplierName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpkToDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
