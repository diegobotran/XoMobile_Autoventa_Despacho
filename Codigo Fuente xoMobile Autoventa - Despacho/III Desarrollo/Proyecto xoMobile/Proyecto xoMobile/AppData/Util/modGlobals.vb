Imports System.Data
Module modGlobals
    Public id_glo_sociedad As String = ""
    Public id_glo_centro As String = ""
    Public id_glo_ruta As String
    Public id_glo_codRuta As String '= "33"
    Public id_glo_cliente As Integer = 5
    Public id_glo_usuario As Integer = 2025
    Public id_glo_producto As Integer
    Public id_glo_dia As Integer = 2
    Public id_glo_clienteGenerico As String = "52"
    Public id_glo_BodegaEnvase As String = "1"
    Public id_glo_BodegaProducto As String = "2"
    Public id_glo_server As String = ""
    Public id_glo_limite As Integer = 0
    Public id_glo_server_fel As String = ""
    Public id_glo_api As String = ""
    Public id_glo_protocolo As String = ""
    Public id_glo_rol As String
    Public id_glo_internet As Boolean = False
    Public id_glo_condicion As String = ""
    Public id_glo_sistema As String = " xoMobile "
    Public id_glo_version As String = " Version 4.0 - 11/05/2023"
    Public gps As New xoMobileGPS.xoMobileGps
    Public id_glo_fel As String
    Public id_glo_total_cf As Integer = 2500
    Public id_glo_cui_hh As String = ""

    '--- Colores
    Public xoWarning As Color = Color.FromArgb(255, 235, 232)
    Public xoInfomat As Color = Color.FromArgb(255, 249, 215)
    Public xoAttention As Color = Color.FromArgb(223, 227, 238)
    Public xoAttentionLabel As Color = Color.FromArgb(59, 89, 152)

    '--- Constantes Globales
    Public co_glo_moneda As String = "QTZ"
    Public co_glo_porcentajeIVA As String = 0.12
    Public co_glo_vendedor As String = "PEDRO COYOY"
    Public co_glo_bodeguero As String = "MARVIN LOPEZ"
    Public co_glo_liquidador As String = "--"
    Public co_glo_wfExitoso As Boolean = False
    Public co_glo_bodegaEnvase As String = "MIXCO-ENVASE VACIO"
    Public co_glo_bodegaPT As String = "MIXCO-PRODUCTO TERMINADO"
    Public co_glo_searchCliente = "descripcion"
    Public co_glo_searchProducto = "descripcion"
    Public searchBy As String = ""
    Public co_glo_fechaCarga As DateTime
    Public co_glo_despacho As Boolean = False
    Public co_glo_confirma_despacho As Boolean = False
    Public tipoRuta As Integer
    Public co_glo_FormFactor As Integer = 1
    Public id_glo_estoy_fuera As Boolean
    Public id_glo_npedidos As Integer = 0

    '--- Controles    
    Public xo_restringeVenta As Boolean = False
    Public xo_restringeCobro As Boolean = False
    Public xo_restringeAnulacion As Boolean = False
    Public xo_LimiteCorrelativoAlcanzado As Boolean = False
    Public xo_validaInventario As Boolean = True
    Public id_glo_aplicacion As String
    Public id_glo_limite_nc As Integer = 0
    Public id_glo_aplicacion2 As String
    Public co_glo_salir As Boolean = True 'Bandera que indica la salida del FormStack atencion
    Public co_glo_NextForm As Boolean
    Public co_glo_closeAndGo As Boolean
    Public dontLoad As Boolean
    Public closenow As Boolean


    '--- UI controles
    Public glo_lvNcProductos, glo_lvNcAgregados, glo_lvVentaProductos, glo_lvVentaAgregados As New Windows.Forms.ListView

    Public glo_dtNcProductos, glo_dtVentaProductos As New DataTable
    Public glo_dt_productos As New DataTable

    '--- Listados publicos
    Public glo_productos_venta_dt As New DataTable
    Public glo_productos_inventario_dt As New DataTable
    Public glo_productos_locks_dt As New DataTable
    Public total_venta As Double
    Public total_envase As Double
    Public envase_permitido As Double

End Module
