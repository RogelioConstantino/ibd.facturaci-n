<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="report-viewer.aspx.cs" Inherits="Ibd.SiMer.Web.report_viewer" %>
        <%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cuerpo" runat="server">

            
        <br />
        <div class="col s12 center">             
            <h5 style="color: #427314 !important;">Reporte de Facturación de Mercado</h5>            
        </div>    
    
        <br />    
        <br />    


        <form id="form1" runat="server">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        </asp:ScriptManagerProxy>        
            <div class="row">                                         
                <div class="input-field col s2 ">
                        <asp:LinkButton ID="lnkExportar" name="lnkExportar"  CssClass="btn olive waves-effect waves-light light-green darken-3 right" runat="server" Text="Exportar"  OnClick="lnkExportar_Click" ClientIDMode="Static"  >                                  
                            <i class="material-icons left">system_update_alt</i>     Exportar
                        </asp:LinkButton>                                             
                </div>     
            </div>

            

<rsweb:ReportViewer ID="MyReportViewer" runat="server" BackColor="" ClientIDMode="AutoID" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Height="100%" ShowParameterPrompts="False" Width="100%" ShowToolBar="False" SizeToReportContent="True">
  <ServerReport ReportServerUrl="" />
    <LocalReport ReportPath="Reportes\rptFactMercado.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DSFacMerc" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SIMERConnectionString %>" SelectCommand="		SELECT 
				   A.Año , A.Mes as mes				
				   ,format(convert(date,'2017-'+convert(varchar,A.Mes)+'-01'),'MMM-yy') nombreMes	
				 , z.ZonasCarga 
				 , p.PuntoCarga 
				,Cargo_energia_CD
				,Cargo_Demanda_SD
				,Cargo_energia_SD
				,Cargo_Demanda_CD				 				 
				,Cargo_Demanda_CD	
				+Cargo_energia_CD	
				+Cargo_Demanda_SD	
				+Cargo_energia_SD
				,Energia		
				,Demanda_Facturable						
		FROM Analitica A JOIN PuntosCarga p on a.PuntodeCarga =  p.IdPuntoCarga
		join zonascarga z on z.IdZonasCarga = p.IdZonaCarga	"></asp:SqlDataSource>
        <asp:SqlDataSource ID="DSFacMerc" runat="server"></asp:SqlDataSource>

    </form>

 

</asp:Content>
