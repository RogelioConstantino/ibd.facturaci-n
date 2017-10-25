using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using Ibd.SiMer.Negocio;

namespace Ibd.SiMer.Web
{
    public partial class configuraciones : System.Web.UI.Page
    {
        #region Variables Generales

        StringBuilder strHTML = new StringBuilder();
        #endregion

        #region Eventos de pagina
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                cargaEmpresa();                
                cargaGrupo();
                cargaClientes();
                cargaPuntosCarga();
                cargaContratos();
                cargaProductos();
                cargaComportamientos();
                cargaComportamientoContrartos();
                cargaFactoresReduccion();
                cargaTarifas();

            }
        }
        #endregion

        #region Metodos
        private void cargaEmpresa()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            configuracionesNe oclsNe = new configuracionesNe();

            dtGR = oclsNe.getEmpresa();            
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsNe.CreateTableHTML_Emp(dtGR);
                DBDataPlaceHolder_Empresa.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }
        private void cargaGrupo()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            configuracionesNe oclsNe = new configuracionesNe();

            dtGR = oclsNe.getGrupo();
            PlaceHolderGpo.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsNe.CreateTableHTML_Gpo(dtGR);
                PlaceHolderGpo.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }

        private void cargaClientes()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            configuracionesNe oclsNe = new configuracionesNe();

            dtGR = oclsNe.getCliente();
            PlaceHolderCte.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsNe.CreateTableHTML_Cte(dtGR);
                PlaceHolderCte.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }

        private void cargaPuntosCarga()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            configuracionesNe oclsNe = new configuracionesNe();

            dtGR = oclsNe.getPuntosCarga();
            PlaceHolderPC.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsNe.CreateTableHTML_PC(dtGR);
                PlaceHolderPC.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }

        private void cargaContratos()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            configuracionesNe oclsNe = new configuracionesNe();

            dtGR = oclsNe.getContratos();
            PlaceHolderCtto.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsNe.CreateTableHTML_Ctto(dtGR);
                PlaceHolderCtto.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }

        private void cargaProductos()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            configuracionesNe oclsNe = new configuracionesNe();

            dtGR = oclsNe.getProductos();
            PlaceHolderCtto.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsNe.CreateTableHTML_Productos(dtGR);
                PlaceHolderProducto.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }

        private void cargaComportamientos()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            configuracionesNe oclsNe = new configuracionesNe();

            dtGR = oclsNe.getComportamientos();
            PlaceHolderCtto.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsNe.CreateTableHTML_Comportamientos(dtGR);
                PlaceHolderComportamientos.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }

        private void cargaComportamientoContrartos()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            configuracionesNe oclsNe = new configuracionesNe();

            dtGR = oclsNe.getComportamientosContratos();
            PlaceHolderCtto.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsNe.CreateTableHTML_Comportamientos(dtGR);
                PlaceHolderComportamientocontratos.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }


        private void cargaFactoresReduccion()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            configuracionesNe oclsNe = new configuracionesNe();

            dtGR = oclsNe.getFactoresReduccion();
            PlaceHolderCtto.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsNe.CreateTableHTML(dtGR);
                PlaceHolderFactoresReduccion.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }


        private void cargaTarifas()
        {
            System.Data.DataTable dtGR = new System.Data.DataTable();
            configuracionesNe oclsNe = new configuracionesNe();

            dtGR = oclsNe.getTarifas();
            PlaceHolderCtto.Controls.Add(new Literal { Text = "" });
            if (dtGR != null && (dtGR.Rows.Count > 0))
            {
                strHTML = oclsNe.CreateTableHTML(dtGR);
                PlaceHolderTarifas.Controls.Add(new Literal { Text = strHTML.ToString() });
            }
        }

        //private void Empresa()
        //{
        //    System.Data.DataTable dtGR = new System.Data.DataTable();
        //    configuracionesNe oclsNe = new configuracionesNe();

        //    dtGR = oclsNe.getEmpresa();
        //    DBDataPlaceHolderIcons.Controls.Add(new Literal { Text = "" });
        //    if (dtGR != null && (dtGR.Rows.Count > 0))
        //    {
        //        strHTML = oclsNe.CreateTableHTML(dtGR);
        //        DBDataPlaceHolderIcons.Controls.Add(new Literal { Text = strHTML.ToString() });
        //    }
        //}







        #endregion


    }
}