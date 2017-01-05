using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Migracion_Facturacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_recib_cob_Click(object sender, EventArgs e)
        {
            Recibos_Cobros obj = new Recibos_Cobros();
            this.Hide();
            obj.Show();
        }

        private void btn_recib_Click(object sender, EventArgs e)
        {
            Recibos obj = new Recibos();
            this.Hide();
            obj.Show();
        }

        private void btn_recib_det_Click(object sender, EventArgs e)
        {
            Recibos_Detalles obj = new Recibos_Detalles();
            this.Hide();
            obj.Show();
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("¿Seguro de querer salir?", "System Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Application.ExitThread();
            }
        }

        private void btn_movimient_Click(object sender, EventArgs e)
        {
            Movimientos_Cobros obj = new Movimientos_Cobros();
            this.Hide();
            obj.Show();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Movimientos_Cobros obj = new Movimientos_Cobros();
            this.Hide();
            obj.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Frm_Credito_Y_Cobranza obj = new Frm_Credito_Y_Cobranza();
            this.Hide();
            obj.Show();
        }

        private void Sanciones_Click(object sender, EventArgs e)
        {
            Recibos_Detalles obj = new Recibos_Detalles();
            this.Hide();
            obj.Show();
        }
    }
}
