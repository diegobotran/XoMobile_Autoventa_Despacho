Imports System.Data
Imports System.Data.SqlServerCe

Public Class DespachoDT

    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable
    Dim Conexion As SqlCeConnection
    Dim objCe As New ceClient
    '---ok

    Public Function getDespachos(ByVal idCliente As String, ByRef rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
         & " SELECT D.serie, D.valor, D.litros, D.tipoPago, ttipo.ShowValue, D.id_despacho, D.despachado, F.id_pedido, sum(FD.isDiferente) as isDiferente, D.estado, negocio, F.importeDesto, F.importeDestoPP,F.condicion, F.tPedido, F.tMotivo" _
         & " FROM [DESPACHO]  D  " _
         & "    LEFT JOIN ttipo on razonNodespacho like datavalue            " _
         & "    and tabla = 'RAZONES_NO_DESPACHO'   " _
         & "        LEFT JOIN cfactura F ON nopedido = D.serie     " _
         & "            LEFT JOIN rcliente ON id_cliente = D.idcliente  " _
         & "                LEFT  JOIN cfactura_detalle FD ON idEncfactura = id_encfactura " _
         & " WHERE D.idCliente  like '" & idCliente & "' AND (F.estado <> 2 OR F.estado is null) AND F.tPedido <> 'ZCAM'" _
         & " GROUP BY D.serie, D.valor, D.litros, D.tipoPago, ttipo.ShowValue, D.id_despacho, D.despachado, F.id_pedido, D.estado, negocio, F.importeDesto, F.importeDestoPP, F.condicion, F.tPedido, F.tMotivo"
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    Public Function getDespachosCambio(ByVal idCliente As String, ByRef rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
         & " SELECT D.serie, 0 valor, D.litros, D.tipoPago, ttipo.ShowValue, D.id_despacho, D.despachado, F.id_pedido, sum(FD.isDiferente) as isDiferente, D.estado, negocio, F.importeDesto, F.importeDestoPP,F.condicion, F.tPedido, F.tMotivo" _
         & " FROM [DESPACHO]  D  " _
         & "    LEFT JOIN ttipo on razonNodespacho like datavalue            " _
         & "    and tabla = 'RAZONES_NO_DESPACHO'   " _
         & "        LEFT JOIN cfactura F ON nopedido = D.serie     " _
         & "            LEFT JOIN rcliente ON id_cliente = D.idcliente  " _
         & "                LEFT  JOIN cfactura_detalle FD ON idEncfactura = id_encfactura " _
         & " WHERE D.idCliente  like '" & idCliente & "' AND (F.estado <> 2 OR F.estado is null) AND F.tPedido = 'ZCAM'" _
         & " GROUP BY D.serie, D.valor, D.litros, D.tipoPago, ttipo.ShowValue, D.id_despacho, D.despachado, F.id_pedido, D.estado, negocio, F.importeDesto, F.importeDestoPP, F.condicion, F.tPedido, F.tMotivo"
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    Public Function updatePedidoDespachado(ByVal idDespacho As String, ByRef rlayer As rLayerHandler, ByVal despachado As String) As Integer
        SQL_QUERY = " " _
        & " UPDATE DESPACHO " _
        & " SET  despachado = '" & despachado & "'" _
        & " ,fechaOpeacion = getdate()" _
        & " ,usuario =" & id_glo_usuario _
        & " ,razonNoDespacho = NULL" _
        & " WHERE idPedido = " & idDespacho

        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
    End Function

    Public Function updateTodosMotivoDespacho(ByVal id_cliente As String, ByRef rlayer As rLayerHandler, ByVal idMotivo As String) As Integer
        SQL_QUERY = " " _
        & " UPDATE DESPACHO " _
        & " SET  razonNoDespacho = '" & idMotivo & "'" _
        & " ,fechaOpeacion = getdate()" _
        & " ,despachado = 'False' " _
        & " ,usuario =" & id_glo_usuario _
        & " WHERE idcliente = " & id_cliente & " and (despachado = 'False' or despachado is null) "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
    End Function


    Public Function updateMotivoDespacho(ByVal id_cliente As String, ByRef rlayer As rLayerHandler, ByVal idMotivo As String, ByVal idDespacho As String) As Integer
        SQL_QUERY = " " _
        & " UPDATE DESPACHO " _
        & " SET  razonNoDespacho = '" & idMotivo & "'" _
        & " ,fechaOpeacion = getdate()" _
        & " ,usuario =" & id_glo_usuario _
        & " WHERE id_despacho = " & idDespacho & " "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
    End Function

    Public Function setConfirmarDespacho(ByRef rlayer As rLayerHandler) As Integer
        SQL_QUERY = " " _
        & " UPDATE DESPACHO " _
        & " SET  estado = 1" _
        & " , fechaConfirma = getdate()" _
        & " , usuarioConfirma =" & id_glo_usuario _
        & " , fechaOpeacion =    CASE WHEN fechaOpeacion IS NULL THEN GETDATE() ELSE  fechaOpeacion  END " _
        & " , razonNodespacho =  CASE WHEN fechaOpeacion IS NULL THEN -1  else razonNoDespacho END "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
    End Function

    '---- Aqui codigo para cambiar el litm
    Public Function setCompletarDespacho(ByRef rlayer As rLayerHandler) As Integer

        '--- Insertar registros complementarios
        SQL_QUERY = " " _
        & " INSERT INTO [CFACTURA_DETALLE]([idEncFactura],[item],[idProducto],[um],[cantidad],[precio],[importeSinIva],[importe],[iva],[porcentajeDesto],[porcentajeDestoPP],[importeDestoPP], " _
        & " [importeDesto],[tipoVenta],[idRubro],[litm],[estado],[valorIvaDesto],[valorIvaImporte],[id_Pedido],[posicionSuperior],[noPedido],[litros],[trqt], [isDiferente], [canDespacho])   " _
        & " SELECT " _
        & " [id_EncFactura],[item],[idProducto],[um],[cantidad],[precio],[importeSinIva],[importe],[iva],[porcentajeDesto],[porcentajeDestoPP],[importeDestoPP] " _
        & " ,[importeDesto],[tipoVenta],[idRubro],[litm],[estado],[valorIvaDesto],[valorIvaImporte],[id_Pedido],[posicionSuperior],[noPedido],[litros]  " _
        & " ,CONVERT(NVARCHAR(10),case when unidadesCaja = 1 then 0 else convert(nvarchar(100),ceiling(floor(Resumen)))end) + '/' + CONVERT(NVARCHAR(10),case when unidadesCaja = 1 then       convert(nvarchar(100),ceiling(floor(Resumen))) else convert(nvarchar(100), ceiling ((Resumen - floor(Resumen))*unidadesCaja)) end) as TRQT , [isDiferente], [canDespacho] " _
        & " FROM   " _
        & " (  " _
        & "	SELECT   a.id_encFactura,b.item,b.idProducto,b.um,b.cantidad,b.precio,b.importeSinIva,b.importe,b.iva,b.porcentajeDesto,b.porcentajeDestoPP  " _
        & "	,CASE WHEN A.CONDICION <> 'IL01' THEN b.importeDestoPP  end importedestoPP  " _
        & "	,CASE WHEN A.CONDICION = 'IL01' THEN b.importeDesto  end importedesto    " _
        & "	,'PED' tipoVenta  " _
        & "	,CASE WHEN P.ttipo = 1 then  'L'   WHEN P.ttipo = 2 THEN 'E'  WHEN P.ttipo = 3 THEN 'C' END as idRubro  " _
        & "	,B.cantidad/convert(decimal,P.unidadesCaja) Resumen   " _
        & "	,idproducto as 	litm,  b.estado,b.valorIvaDesto,b.valorIvaImporte,b.id_Pedido, posicionSuperior,A.noPedido,CASE WHEN p.ttipo = 1 then b.litros  END as   litros, unidadesCaja  , [isDiferente], [canDespacho]" _
        & "	FROM cfactura a left join cfactura_detalle b  on a.id_pedido = b.id_pedido    " _
        & "	left join RPRODUCTO P ON id_producto = idproducto   " _
        & " )qa  "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)

        '--- Eliminar registros base
        If rlayer.codigo = 0 Then
            SQL_QUERY = "   DELETE FROM  cfactura_detalle WHERE idrubro is null "
            SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
        End If


        '--- Crear Tabla control de pedidos
        If rlayer.codigo = 0 Then
            SQL_QUERY = "   INSERT INTO [DESPACHO]([idCliente],[numero],[serie],[valor],[litros],[tipoPago],[idPedido])  " _
            & " SELECT A.idcliente,0 numero,A.noPedido,A.importe,sum(CONVERT(DECIMAL(18,3),litros))  litros, CASE WHEN condicion = 'IL01' then 'CONTADO' ELSE 'CREDITO' END AS tipoPago, a.id_pedido" _
            & " FROM  " _
            & " CFACTURA A LEFT JOIN CFACTURA_DETALLE  B " _
            & " ON B.id_pedido = A.id_pedido  " _
            & " GROUP BY  idcliente,A.noPedido,importe,condicion,id_pedido "
            SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
        End If

    End Function

    Public Function getPedidoById(ByVal id_pedido As String, ByRef rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
        & " SELECT * FROM CFACTURA " _
        & " WHERE id_pedido = " & id_pedido & " AND ( estado <> 2 or estado is null )"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    Public Function getPedidoDetalleById(ByVal id_pedido As String, ByRef rlayer As rLayerHandler) As DataTable
        SQL_QUERY = " " _
          & " SELECT  " _
          & " idProducto, descripcion,idRubro, trqt, precio, importe,           " _
          & " case when importeDesto  is null then importeDestoPP  else importeDesto   end as ImporteDesto  " _
          & " FROM CFACTURA_DETALLE left join rproducto  " _
          & " on id_producto = idproducto " _
          & " WHERE id_pedido = " & id_pedido _
          & " order by item "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY, rlayer)
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

End Class
