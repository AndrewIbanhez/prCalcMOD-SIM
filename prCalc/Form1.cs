using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prCalc
{
    public partial class Form1 : Form
    {
        Double n = 0;
        Double p = 0;
        Double N = 0;
        Double T = 0;
        Double L = 0;
        List<Double> arrx = new List<Double>();
        bool bHG = false;
        bool bB = false;
        bool bP = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void grpBinomial_Enter(object sender, EventArgs e)
        {
        }

        public Double distBin(int n, Double x, Double p, Double q)
        {
            Console.WriteLine("It.0" +factorial(x) * factorial(n - x));
            return (Double)(factorial(n) / (factorial(x) * factorial(n - x))) * Math.Pow(p, x) * Math.Pow(q, n -x) ;
        }

        public BigInteger factorial(Double nu)
        {
            if (nu == 0 || nu == 1)
            {
                return 1;
            }
            BigInteger ini = 1;
            for (Double i = 1; i <= nu; i++)
            {
                ini = ini * (BigInteger)i;
            }
            return ini;
        }

        public void graficar()
        {
            
            OxyPlot.WindowsForms.PlotView pv = new PlotView();
            pv.Location = new Point(5,10);
            pv.Size = new Size(grpPlot.Width - 10, grpPlot.Height-10);
            grpPlot.Controls.Add(pv);
            pv.Model = new PlotModel { };
            FunctionSeries fs = new FunctionSeries();
            for (int i = 0; i < dgvx.Rows.Count-1; i++)
            {
                fs.Points.Add(new DataPoint(Convert.ToDouble(dgvx.Rows[i].Cells[0].Value), Convert.ToDouble(dgvx.Rows[i].Cells[1].Value)));
            }
            pv.Model.Series.Add(fs);
        }
        
        private void btnDB_Click(object sender, EventArgs e)
        {
            dgvx.Rows.Clear();
            try
            {
                bP = false;
                bHG = false;
                bB = true;
                this.n = Convert.ToDouble(txtN.Text);
                this.p = Convert.ToDouble(txtP.Text);
                if (dgvx.Rows[0].Cells[0].Value == null)
                {
                    for (int i = 0; i <= n; i++)
                    {
                        dgvx.Rows.Add();
                        dgvx.Rows[i].Cells[0].Value = i;
                    }
                }
                else 
                {
                    arrx.Clear();
                    for (int i = 0; i < dgvx.Rows.Count; i++)
                    {
                        arrx.Add(Convert.ToDouble(dgvx.Rows[i].Cells[0].Value));
                    }
                    arrx.Sort();
                    for (int i = 0; i < dgvx.Rows.Count; i++)
                    {
                        dgvx.Rows[i].Cells[0].Value = arrx.ElementAt(i);
                    }
                }
                for (int i = 0; i < dgvx.Rows.Count; i++)
                {
                    dgvx.Rows[i].Cells[1].Value = distBin((Int32)n, Convert.ToDouble(dgvx.Rows[i].Cells[0].Value), p, (1 - p));
                }
                graficar();
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese valores válidos para n y p", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grpPlot_Enter(object sender, EventArgs e)
        {

        }
               private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtN_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnDE_Click(object sender, EventArgs e)
        {
            if (bHG)
            {
                n = Convert.ToDouble(txtN.Text);
                N = Convert.ToDouble(txtNPob.Text);
                T = Convert.ToDouble(txtT.Text);
                lblFC.Text = Math.Sqrt((Double)(N - n) / (Double)(N - 1)).ToString();
                lblDE.Text = Convert.ToString(Math.Sqrt(n * T / N * (1 - (T / N))) * Math.Sqrt((Double)(N - n) / (Double)(N - 1)));
            }
            else if (bB)
            {
                n = Convert.ToInt32(txtN.Text);
                p = Convert.ToDouble(txtP.Text);
                lblM.Text = Convert.ToString(n * p);
                if (txtN.Text.Equals(""))
                {
                    lblDE.Text = Convert.ToString(Math.Sqrt((Double)n * (Double)p * (Double)(1 - p)));
                }
                else if ((Int32)n > (BigInteger)(Convert.ToInt32(txtN.Text) * 0.05))
                {
                    lblDE.Text = Convert.ToString(Math.Sqrt((Double)n * (Double)p * (Double)(1 - p)));
                }
                else
                {
                    N = Convert.ToInt32(txtNPob.Text);
                    lblDE.Text = (Convert.ToDouble(lblFC.Text) * Convert.ToDouble(lblFC.Text)).ToString();
                }
            }
            else if(bP)
            {
                lblDE.Text = Convert.ToString(Math.Sqrt(L));
            }

        }

        private void calcMedia()
        {
            if (bHG)
            {
                n = Convert.ToDouble(txtN.Text);
                T = Convert.ToDouble(txtT.Text);
                N = Convert.ToDouble(txtNPob.Text);
                lblM.Text = Convert.ToString((n * T) / N);
            }
            else if (bB || bP)
            {
                this.n = Convert.ToInt32(txtN.Text);
                this.p = Convert.ToDouble(txtP.Text);
                lblM.Text = Convert.ToString(n * p);
            }
            else
            {
                MessageBox.Show("No se ha realizado ningún cálculo de distribución", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnM_Click(object sender, EventArgs e)
        {
            calcMedia();
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ordenar(List<Double> arrX)
        {
            Double aux;
            for (int i = 1; i < arrX.Count; i++)
                for (int j = arrX.Count - 1; j >= i; j--)
                {
                    if (arrX[j] < arrX[j - 1])
                    {
                        aux = arrX[j];
                        arrX[j] = arrX[j - 1];
                        arrX[j - 1] = aux;
                    }
                }
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            try
            {
                n = Convert.ToDouble(txtN.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un valor válido para n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (bB)
            {
                p = Convert.ToDouble(txtP.Text);
                calcSes();
            }
            else if (bHG)
            {
                p = T / N;
                calcSes();
            }
            else if (bP)
            {
                p = L / n;
                calcSes();
            }
            else
            {
                MessageBox.Show("Antes de calcular el sesgo, debe calcular una distribución", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        public void calcSes()
        {
            lblMD.Text = "--";
            lblS.Text = "--";
            lblM.Text = "--";
            rbNul.Checked = false;
            rbPos.Checked = false;
            rbNeg.Checked = false;

            Console.WriteLine(dgvx.RowCount);

            if (dgvx.Rows[0].Cells[0] == null)
            {
                for (int i = 0; i < n; i++)
                {
                    dgvx.Rows.Add(i, i, 0);
                    arrx.Add(i);
                }
            }
            else
            {
                for (int i = 0; i < dgvx.Rows.Count; i++)
                {
                    arrx.Add((Convert.ToDouble(dgvx.Rows[i].Cells[0].Value)));
                }
                ordenar(arrx);
                for (int i = 0; i < dgvx.Rows.Count; i++)
                {
                    dgvx.Rows[i].Cells[0].Value = arrx.ElementAt(i);
                }
            }

            for (int i = 0; i < dgvx.Rows.Count; i++)
            {
                dgvx.Rows[i].Cells[1].Value = distBin((Int32)n, Convert.ToDouble(dgvx.Rows[i].Cells[0].Value), p, (1 - p));
            }
            graficar();

            lblS.Text = Convert.ToString((Double)((1 - p) - p) / Math.Sqrt((Double)n * (Double)p * (Double)(1 - p)));
            if (arrx.Count % 2 == 0)
            {
                lblMD.Text = Convert.ToString((arrx.ElementAt(arrx.Count / 2) + arrx.ElementAt((arrx.Count / 2) + 1)) / 2);
            }
            else
            {
                lblMD.Text = Convert.ToString(arrx.ElementAt(arrx.Count / 2));
            }
            calcMedia();
           // lblM.Text = Convert.ToString(Math.Sqrt((Double)n * (Double)p * (Double)(1 - p)));

            if (Convert.ToDouble(lblM.Text) < Convert.ToDouble(lblMD.Text))
                rbNeg.Checked = true;
            else if (Convert.ToDouble(lblM.Text) > Convert.ToDouble(lblMD.Text))
                rbPos.Checked = true;
            else
                rbNul.Checked = true;
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            try
            {
                n = Convert.ToDouble(txtN.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese un valor válido para n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (bB)
            {
                p = Convert.ToDouble(txtP.Text);
                calcCur();
            }
            else if (bHG)
            {
                p = T / N;
                calcCur();
            }
            else if (bP)
            {
                p = L / n;
                calcCur();
            }
            else
            {
                MessageBox.Show("Antes de calcular la curtosis, debe calcular una distribución", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void calcCur()
        {
            n = Convert.ToInt32(txtN.Text);
            p = Convert.ToDouble(txtP.Text);
            lblC.Text = (3 + ((1 - (6 * p * (1 - p))) / (Math.Sqrt(n * p * (1 - p))))).ToString();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnPrcTm_Click(object sender, EventArgs e)
        {
            if (!bHG)
            {
                MessageBox.Show("Ingrese un valor para el tamaño de la muestra y para el tamaño de la población", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                n = Convert.ToInt32(txtN.Text);
                N = Convert.ToInt32(txtNPob.Text);
                lblPrc.Text = Convert.ToString(n * 100 / N);           
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!bHG)
            {
                MessageBox.Show("Ingrese un valor para el tamaño de la muestra y para el tamaño de la población", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                n = Convert.ToDouble(txtN.Text);
                N = Convert.ToDouble(txtNPob.Text);
                double fc = (N - n) / (N - 1);
                lblFCtm.Text = Math.Pow(fc, 0.5).ToString();
            }
        }

        private void btnHiperG_Click(object sender, EventArgs e)
        {
            try
            {
                bHG = true;
                bB = false;
                bP = false;
                dgvx.Sort(dgvx.Columns[0], ListSortDirection.Ascending);
                T = Convert.ToDouble(txtT.Text);
                n = Convert.ToDouble(txtN.Text);
                N = Convert.ToDouble(txtNPob.Text);
                if (dgvx.Rows[0].Cells[0].Value == null)
                {
                    for (int i = 0; i <= n; i++)
                    {
                        dgvx.Rows.Add();
                        dgvx.Rows[i].Cells[0].Value = i;
                    }
                }
                else 
                {
                    arrx.Clear();
                    for (int i = 0; i < dgvx.Rows.Count; i++)
                    {
                        arrx.Add(Convert.ToDouble(dgvx.Rows[i].Cells[0].Value));
                    }
                    arrx.Sort();
                    for (int i = 0; i < dgvx.Rows.Count; i++)
                    {
                        dgvx.Rows[i].Cells[0].Value = arrx.ElementAt(i);
                    }
                }
                for (int i = 0; i < dgvx.Rows.Count; i++)
                {
                    dgvx.Rows[i].Cells[1].Value = ((Double)factorial(N - T) * (Double)factorial(T) * (Double)factorial(n) * (Double)factorial(N - n)) / ((Double)factorial(n - Convert.ToDouble(dgvx.Rows[i].Cells[0].Value)) * (Double)factorial(N - T - n + Convert.ToDouble(dgvx.Rows[i].Cells[0].Value)) * (Double)factorial(Convert.ToDouble(dgvx.Rows[i].Cells[0].Value)) * (Double)factorial(T - Convert.ToDouble(dgvx.Rows[i].Cells[0].Value)) * (Double)factorial(N));
                }
                graficar();
            }
            catch (Exception)
            {

                MessageBox.Show("Ingrese valores válidos para n, N, T", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnPoi_Click(object sender, EventArgs e)
        {

                bHG = false;
                bB = false;
                bP = true;
                if (txtLam.Text.Equals(null) || txtLam.Text.Equals(""))
                {
                    try
                    {

                        p = Convert.ToDouble(txtP.Text);
                        n = Convert.ToDouble(txtN.Text);
                        L = n * p;
                        txtLam.Text = Convert.ToString(L);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Ingrese un valor válido para n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    L = Convert.ToDouble(txtLam.Text);
                    txtLam.Text = L.ToString();
                }

            if (dgvx.Rows[0].Cells[0].Value == null)
            {
                try
                {
                    n =Convert.ToDouble(txtN.Text);
                    for (int i = 0; i <= n; i++)
                    {
                        dgvx.Rows.Add();
                        dgvx.Rows[i].Cells[0].Value = i;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Debe ingresar un valor a n o llenar la tabla de probabilidades");
                }
            }
            else
            {
                arrx.Clear();
                for (int i = 0; i < dgvx.Rows.Count; i++)
                {
                    arrx.Add(Convert.ToDouble(dgvx.Rows[i].Cells[0].Value));
                }
                arrx.Sort();
                for (int i = 0; i < dgvx.Rows.Count; i++)
                {
                    dgvx.Rows[i].Cells[0].Value = arrx.ElementAt(i);
                }
            }
            for (int i = 0; i < dgvx.Rows.Count; i++)
            {
                dgvx.Rows[i].Cells[1].Value = (Math.Pow(L, Convert.ToDouble(dgvx.Rows[i].Cells[0].Value)) / ((Double)factorial(Convert.ToDouble(dgvx.Rows[i].Cells[0].Value)) * Math.Pow(Math.E, L)));

            }
            graficar();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bHG = false;
            bB = false;
            bP = false;
            dgvx.Rows.Clear();
        }

        private void btnLG_Click(object sender, EventArgs e)
        {
            grpPlot.Controls.Clear();
        }

        private void dgvx_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void grpMM1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnMM1_Click(object sender, EventArgs e)
        {
            try
            {
                Double TLL = Convert.ToDouble(txtTLL.Text);
                Double RS = Convert.ToDouble(txtRS.Text);
                lblWq.Text = (TLL / (RS * (RS - TLL))).ToString();
                lblWs.Text = (1 / (RS - TLL)).ToString();
                lblLs.Text = (TLL / (RS - TLL)).ToString();
                lblLq.Text = (Math.Pow(TLL,2)/(RS*(RS-TLL))).ToString();
                lblPor.Text = (TLL / RS).ToString();
                try
                {
                    n = Convert.ToDouble(txtNprob.Text);
                    lblProbn.Text = ((1 - (TLL / RS)) * Math.Pow(TLL / RS, n)).ToString();
                }
                catch (Exception)
                {

                    lblProbn.Text = (1 - (TLL / RS)).ToString();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ingrese los valores correctos para realizar el cálculo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}