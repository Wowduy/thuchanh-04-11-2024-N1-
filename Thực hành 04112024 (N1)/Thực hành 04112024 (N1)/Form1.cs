using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thực_hành_04112024__N1_
{
    public partial class Form1 : Form
    {
        private List<Customer> customers = new List<Customer>();
        private List<Service> services = new List<Service>();
        private List<Service> selectedServices = new List<Service>();

        public Form1()
        {
            InitializeComponent();
            InitializeData();
            LoadCustomersToGrid();
            LoadServicesToGrid();
        }

        private void InitializeData()
        {
            customers.Add(new Customer { Id = 1, Name = "Nguyễn Văn A", Phone = "0123456789", Address = "Hà Nội" });
            customers.Add(new Customer { Id = 2, Name = "Trần Thị B", Phone = "0987654321", Address = "TP Hồ Chí Minh" });

            services.Add(new Service { Code = "DV01", Name = "Dịch vụ 1", Price = 100000 });
            services.Add(new Service { Code = "DV02", Name = "Dịch vụ 2", Price = 200000 });
            services.Add(new Service { Code = "DV03", Name = "Dịch vụ 3", Price = 300000 });
        }

        // Tải danh sách khách hàng lên DataGridView
        private void LoadCustomersToGrid()
        {
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = customers;
            dgvCustomers.Refresh();
        }

        // Tải danh sách dịch vụ lên DataGridView
        private void LoadServicesToGrid()
        {
            dgvServices.DataSource = null;
            dgvServices.DataSource = services;
        }

        // Thêm khách hàng mới
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
           
            Customer duy = new Customer()
            {
                Id = 4,
                Name = textBox1.Text,
                Phone = textBox2.Text,
                Address = textBox3.Text
            };
            customers.Add(duy);
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = customers;
            dgvCustomers.Refresh();
        }

        // Chỉnh sửa thông tin khách hàng
        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                var selectedCustomer = (Customer)dgvCustomers.SelectedRows[0].DataBoundItem;
                selectedCustomer.Name = "Tên mới đã sửa";
                selectedCustomer.Phone = "SĐT mới đã sửa";
                selectedCustomer.Address = "Địa chỉ mới đã sửa";
            }
        }

        // Xóa khách hàng
        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow != null )
            {
                int s = dgvCustomers.CurrentRow.Index;
                customers.RemoveAt(s);
                LoadCustomersToGrid();
            }
        }

        // Thêm dịch vụ vào hóa đơn
        private void btnAddServiceToInvoice_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count > 0)
            {
                var selectedService = (Service)dgvServices.SelectedRows[0].DataBoundItem;
                selectedServices.Add(selectedService);

                // Hiển thị dịch vụ đã chọn vào ListView
                ListViewItem item = new ListViewItem(selectedService.Code);
                item.SubItems.Add(selectedService.Name);
                item.SubItems.Add(selectedService.Price.ToString("C"));
                lvSelectedServices.Items.Add(item);

                // Cập nhật tổng tiền
                UpdateTotalAmount();
            }
        }

        // Tính và hiển thị tổng tiền hóa đơn
        private void UpdateTotalAmount()
        {
            decimal totalAmount = selectedServices.Sum(service => service.Price);
            lblTotalAmount.Text = $"Tổng tiền: {totalAmount:C}";
        }

        // Tạo hóa đơn
        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {

        }

        private void dgvServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

    // Lớp Khách hàng
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    // Lớp Dịch vụ
    public class Service
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}

