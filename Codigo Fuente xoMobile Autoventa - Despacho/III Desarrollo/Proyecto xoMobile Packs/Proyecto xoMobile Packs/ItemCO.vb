Public Class ItemCO
    Public idEncabezado, _
            NoItem, _
            idProducto, _
            cantidad, _
            cj, _
            un, _
            um, _
            precio, _
            importe, _
            importeSinIva, _
            iva, _
            tipoVenta, _
            idRubro, _
            trqt, cajas, unidades, _
            litm, _
            estado, _
            descripcion, _
            saldo, valorIvaImporte, valorIvaDesto, posicionSuperior As String

    '--CH_TEMP
    Public precioUnitario, precioUm As String

    '--- Producto en unidades 
    Public un_envase As String
    Public un_caja As String
    Public un_liquido As String

    '--- Producto en valores
    Public precioVentaLiquido, precioVentaEnvase, precioVentaCaja, importeLiquidoIva, importeEnvaseIva, importeCajaIva, importeLiquido, importeCaja, importeEnvase, precioUnitarioLiquido, precioUnitarioEnvase, precioUnitarioCaja As Decimal
    Public porcentajeDestoPP As String = "0"
    Public porcentajeDesto As String = "0"
    Public porcentajeDestoFEL As String = "0"
    Public importeDesto As Decimal = 0
    Public importeDestoPP As Decimal = 0
    Public importeDestoFEL As Decimal = 0
    Public importeDestoPPFEL As Decimal = 0

    '--- Atributos del producto
    Public unidadesCaja As String

End Class
