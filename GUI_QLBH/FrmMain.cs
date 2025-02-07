using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLBH
{
    public partial class FrmMain : Form
    {
        public static int session = 0;//kiem tra tình trạng login
        public static int profile = 0;// 
        public static string mail;// truyên email từ frmMain cho các form khác thong qua bien static
        Thread th;//using System.Threading;
        FrmDangNhap dn = new FrmDangNhap();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Resetvalue();
            if (profile == 1)// nếu vừa câp nhật mật khẩu thì 
                             //phải login lại, mục 'thong tin nhan vien' ẩn
            {
                thongtinnvToolStripMenuItem.Text = null;
                profile = 0; //ẩn mục 'thong tin nhan vien'
            }
        }

        //show form đăng nhập
        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExistForm("FrmDangNhap"))
            {
                if (dn == null || dn.IsDisposed) // Kiểm tra xem form đã bị huỷ chưa
                {
                    dn = new FrmDangNhap();
                    dn.MdiParent = this;
                }
                dn.Show();
                dn.FormClosed += new FormClosedEventHandler(FrmDangNhap_FormClosed);
            }
            else
            {
                ActiveChildForm("FrmDangNhap");
            }
        }
        //show form thông tin nv
        private void ProfileNvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmThongTinNV profilenv = new FrmThongTinNV(FrmMain.mail);// khơi tạo FrmThongTinNV với email nv

            if (!CheckExistForm("frmThongTinNV"))
            {
                profilenv.MdiParent = this;
                profilenv.FormClosed += new FormClosedEventHandler(FrmThongTinNV_FormClosed);
                profilenv.Show();
            }
            else
                ActiveChildForm("frmThongTinNV");
        }
        //CheckExistForm để kiểm tra xem 1 Form với tên nào đó đã hiển thị trong Form Cha (frmMain) chưa? => Trả về True (đã hiển thị) hoặc False (nếu chưa hiển thị).
        private bool CheckExistForm(string name)
        {
            bool check = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == name)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }

        //ActiveChildForm dùng để “Kích hoạt”  – hiển thị lên trên cùng các trong số các Form Con nếu nó đã hiện mà không phải tạo thể hiện mới.
        private void ActiveChildForm(string name)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == name)
                {
                    if (!frm.IsDisposed) // Kiểm tra xem form có bị huỷ chưa
                    {
                        frm.Activate();
                    }
                    break;
                }
            }
        }


        //chuc nang nhan vien bình thường ko hiên thị quan lý nhan vien và thống kê
        private void VaiTroNV()
        {
            NhanVienToolStripMenuItem.Visible = false;
            thongkeToolStripMenuItem.Visible = false;
        }

        //Thiệt lap phan quyên khi load frmMain
        private void Resetvalue()
        {
            if (session == 1)
            {
                thongtinnvToolStripMenuItem.Text = "Chào " + FrmMain.mail;
                NhanVienToolStripMenuItem.Visible = true;
                danhMụcToolStripMenuItem.Visible = true;
                LoOutToolStripMenuItem1.Enabled = true;
                thongkeToolStripMenuItem.Visible = true;
                ThongKeSPToolStripMenuItem.Visible = true;
                ProfileNvToolStripMenuItem.Visible = true;
                đăngNhậpToolStripMenuItem.Enabled = false;
                if (int.Parse(dn.vaitro) == 0)//nêu la vai tro nhan vien
                {
                    VaiTroNV(); //chuc nang nhan vien bình thường
                }
            }
            else
            {
                NhanVienToolStripMenuItem.Visible = false;
                danhMụcToolStripMenuItem.Visible = false;
                LoOutToolStripMenuItem1.Enabled = false;
                ProfileNvToolStripMenuItem.Visible = false;
                ThongKeSPToolStripMenuItem.Visible = false;
                thongkeToolStripMenuItem.Visible = false;
                đăngNhậpToolStripMenuItem.Enabled = true;
            }
        }


        void FrmDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            //when child form is closed, this code is executed        
            
            this.Refresh();
            FrmMain_Load(sender, e);// load form main again
        }


        void FrmThongTinNV_FormClosed(object sender, FormClosedEventArgs e)
        {
            //when child form is closed, this code is executed
            this.Refresh();
            FrmMain_Load(sender, e);// load form main again
        }
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoOutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            thongtinnvToolStripMenuItem.Text = null;
            session = 0;
            Resetvalue();
        }

    }
}
