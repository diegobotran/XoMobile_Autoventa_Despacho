Imports System.Data
Public Class documentoCO

    Public idEncabezado As String = 0
    Public serie As String
    Public numero As String
    Public importe As String = 0
    Public porcentajeDesto As String = 0
    Public importeDestoEnv As String = 0
    Public doTipo As String = ""
    Public estado As String = ""
    Public det_dTipo As String
    Public dgPagos As DataTable
    Public idViaPago, documento, idInstitucion, moneda As String
    Public doContinue As Boolean

    '--- Recibo
    Public saldo As String = 0
    Public importePago As String = 0
    Public importeViaPago As String
    Public aceptaDescuento As Boolean = False
    Public aceptaAbono As Boolean = False
    Public idEncFacturaRelacionada As String = 0
    Public idEncCxcRelacionada As String = 0
    Public pagoEfectivo As String
    Public montoTotal As String

    '--- Nota de Credito
    Public ttipo As String
    Public tipoDevolucion As String
    Public porcentajeIva As String


    '--- Factura
    Public ruta, idCliente, nit, idusuario As String
    Public usuarioAnula As String = ""
    Public importeLiquido As String = "0"
    Public motivoAnula As String = ""

    ' Desgloce de descuentos
    Public descuentoProductos As Decimal = 0
    Public descuentoPagoContado As String = "0"

    ' Clasificacion del descuento (Aplicado o futuro [DPP])
    Public importeDesto As String = 0
    Public importeDestoPP As String = 0

    ' Propiedades 
    Public nImpresiones As String = 0
    Public porcentajeDestoPP As String = 0
    Public porcentajeDestoAdicional As String = 0
    Public noResolucion, fechaResolucion, inicial, final, diasVencidos As String
    Public soloEfectivo As Boolean
    Public soloAbono As Boolean
    Public condicion As String = ""
    Public fechaEmision, fechaVence, fechaAnula As String
    Public idReciboRelacionado As String


    '--- PEDIDO
    Public idPedido, noEntrega, noPedido As String
    Public litrosAcumulados As Decimal
    Public descuentoLiquido As String
    Public isDiferente As Boolean = False
    Public isPedido As Boolean = False
    Public isContado As Boolean = True
    Public tipoPago As String = ""

    Public importePedido, importeDespacho, destoDespacho, destoPedido As Decimal
    Public serieFEL, preimpreso, UUID As String
    Public motivo As String = ""
    Public direccionfel As String = ""
    Public diasVencidosE As Integer = 0
    Public numeroacceso As String = ""
    Public tipoReceptor As String = ""
    Public idreceptor As String = ""
    Public nombre_fel As String = ""




End Class

