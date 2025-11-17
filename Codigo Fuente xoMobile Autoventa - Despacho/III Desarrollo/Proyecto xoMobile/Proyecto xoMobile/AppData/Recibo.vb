Imports System.Data
Imports System.Data.SqlServerCe
Imports Proyecto_xoMobile_Packs

Public Class Recibo
    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable
    Dim SQL_DT2 As DataTable
    Dim SQL_DT3 As DataTable
    Dim SQL_DT4 As DataTable
    Dim SQL_DTS As DataTable
    Dim Conexion As SqlCeConnection
    Dim objCe As New ceClient

    '--- ok
    Public Function getDocumentosPorcobrar() As DataTable
        SQL_QUERY = _
        "   SELECT  seleccionado, soloefectivo,id_cxc,idCliente,dtipo,serie,numero,saldo,fechaemision,fechaVence,diasvencidos,importe,sum(importedesto)importedesto,compromisoPago, serieFel, numeroautorizacion, DiasVencidosE " _
        & " FROM " _
        & " ( " _
        & "	    SELECT   " _
        & "	    0 seleccionado, 0 soloEfectivo, id_cxc, idCliente, dTipo, serie, numero, saldo, CONVERT(NVARCHAR(25),fechaEmision,103) fechaEmision , " _
        & "	    CONVERT(NVARCHAR(25),fechaVence,103) fechaVence, 	datediff (dd,fechaVence,GETDATE())DiasVencidos,e.importe, d.importeDesto,compromisoPago, serieFel, numeroautorizacion, datediff (dd,fechaEmision,GETDATE())DiasVencidosE   " _
        & "	    FROM rcxc  e left join rcxc_detalle d on  d.idcxc=e.id_cxc " _
        & "	    WHERE			idCliente		= " & id_glo_cliente.ToString() & "	  " _
        & "	    AND saldo <> 0 " _
        & " )QA " _
        & " GROUP BY seleccionado, soloefectivo,id_cxc,idCliente,dtipo,serie,numero,saldo,fechaemision,fechaVence,diasvencidos,importe,compromisoPago, serieFel, numeroautorizacion, DiasVencidosE"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getNPedidosCliente(ByVal codigo_cliente As Integer) As DataTable
        SQL_QUERY = " select count(f.id_encFactura) as pedidos  from cfactura f, rcliente c where f.idcliente = c.id_Cliente  and f.estado = 1 and c.nit in ('C/F','CF','c/f','cf','c-f','C-F') and len(c.numeroDI) =0  and f.idCliente = '" & codigo_cliente & "'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getDocumentosEncabezadoCXC() As DataTable
        SQL_QUERY = _
        "   SELECT  *, 'NO' pagare  " _
        & " FROM  RCXC "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getDocumentosPorcobrarDetalle(ByVal id_cxc As String) As DataTable
        SQL_QUERY = _
        " SELECT  * " _
        & " FROM RCXC_DETALLE " _
        & " WHERE idCxc 	=	'" & id_cxc.ToString() & "'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function crearEncabezado(ByVal comRecibo As documentoCO) As String

        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Conexion.Open()

        '--- Query Validar Correlativo: Verifica que no exista el correlativo en el sistema, de lo contrario incrementará en 1 el correlativo al max de los registros.

        '--- Correlativo maximo de la tabla cRecibo
        SQL_QUERY = "SELECT MAX(rNumero) FROM crecibo"
        SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Correlativo maximo de la tabla cRecibo
        SQL_QUERY = "SELECT MAX(actual) FROM rccorrelativo WHERE   dtipo = 'R'"
        SQL_DTS = objCe.ExecuteIdentity(SQL_QUERY, Conexion)



        If (SQL_DT.Rows(0).Item(0).ToString() = SQL_DTS.Rows(0).Item(0).ToString()) Then
            MessageBox.Show("Correlativo Repetido : " + SQL_DT.Rows(0).Item(0).ToString())
            '--- Query 3: Incrementar el correlativo
            SQL_QUERY = "UPDATE rccorrelativo SET actual = actual + 1 WHERE  dtipo = 'R'"
            objCe.SetExecute(SQL_QUERY)
        End If


        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction

        Try
            '--- Query 1: insertar encabezado
            SQL_QUERY = _
            " INSERT INTO  CRECIBO ( idRuta, rSerie, rNumero, fechaEmision, idCliente, importe, importeDesto, importeDestoEnv, idusuario, doTipo, estado, nimpresiones, idEncCxc, idEncFactura)" _
            & " SELECT  " & id_glo_ruta.ToString() & ",serie, actual, getdate()," & id_glo_cliente.ToString() & "," & comRecibo.importe & "," & comRecibo.importeDesto & "," & comRecibo.importeDestoEnv & "," & id_glo_usuario.ToString() & ",'" & comRecibo.doTipo & "'," & comRecibo.estado & ",0,'" & comRecibo.idEncCxcRelacionada & "'," & comRecibo.idEncFacturaRelacionada _
            & " FROM    rccorrelativo 	" _
            & " WHERE   dtipo = 'R' "
           
            objCe.ExecuteIdentity(SQL_QUERY, Conexion)

            '--- Query 2: Obtener el id del encabezado
            SQL_QUERY = "SELECT  @@IDENTITY as nn"
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
            objCe.SetExecute(SQL_QUERY)

            '--- Query 3: Incrementar el correlativo
            SQL_QUERY = "UPDATE rccorrelativo SET actual = actual + 1 WHERE  dtipo = 'R'"
            objCe.SetExecute(SQL_QUERY)

            '--- Devolver el id del encabezado
            'tx.Commit()
            Return SQL_DT.Rows(0).Item(0).ToString
        Catch
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en el recibo")
            Return 0
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function

    Public Function cantRecibo(ByVal comRecibo As documentoCO) As String

        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        '--- Query 2: Obtener el id del encabezado
        SQL_QUERY = "SELECT  count(id_encRecibo) cantidad FROM crecibo WHERE importe =" & comRecibo.importe & " AND idcliente = " & id_glo_cliente.ToString() & " AND doTipo = '" & comRecibo.doTipo & "' AND estado = 1"
        SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
        objCe.SetExecute(SQL_QUERY)

        '--- Cerrar la conexion
        Conexion.Close()
        'MsgBox("Se duplico el recibo del cliente " & id_glo_cliente.ToString())
        '--- Devolver el id del encabezado
        Return SQL_DT.Rows(0).Item(0).ToString

    End Function

    Public Function cantidadRecibo(ByVal idRecibo As String) As String

        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        '--- Query 2: Obtener el id del encabezado
        SQL_QUERY = "SELECT  count(id_encRecibo) FROM crecibo WHERE id_encRecibo =" & idRecibo
        SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
        objCe.SetExecute(SQL_QUERY)

        '--- Cerrar la conexion
        Conexion.Close()
        'MsgBox("Se duplico el recibo del cliente " & id_glo_cliente.ToString())
        '--- Devolver el id del encabezado
        Return SQL_DT.Rows(0).Item(0).ToString

    End Function

    Public Function getReciboFact(ByVal idFactura As String) As String

        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        '--- Query 2: Obtener el id del encabezado
        SQL_QUERY = "SELECT  idEncRecibo FROM cFactura WHERE estado = 1 AND id_encFactura =" & idFactura
        SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
        objCe.SetExecute(SQL_QUERY)

        '-- Query 3: 
        SQL_QUERY = "SELECT  count(id_encRecibo)  FROM cRecibo WHERE estado = 1 AND id_encRecibo  =" & SQL_DT.Rows(0).Item(0).ToString
        SQL_DT2 = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
        objCe.SetExecute(SQL_QUERY)

        '--- Devolver si encontrol recibo como verdadero de lo contrario falso
        If (SQL_DT2.Rows(0).Item(0).ToString > 0) Then
            Return SQL_DT.Rows(0).Item(0).ToString
        Else
            Return 0
        End If

        '--- Cerrar la conexion
        Conexion.Close()

    End Function

    Public Function VerificaReciboFact(ByVal idRecibo As String, ByVal idFactura As String, ByVal idCliente As String) As Boolean

        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        '--- Query 1: Obtener el id del encabezado
        SQL_QUERY = "SELECT  idEncFactura FROM cRecibo WHERE idcliente = '" & idCliente & "' AND estado = 1 AND  id_encRecibo =" & idRecibo
        SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
        objCe.SetExecute(SQL_QUERY)

        '-- Query 2: 
        SQL_QUERY = "SELECT  id_encFactura  FROM cFactura WHERE idcliente = '" & idCliente & "' AND estado = 1 AND id_encFactura  =" & idFactura
        SQL_DT2 = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
        objCe.SetExecute(SQL_QUERY)

        '--- Si son diferentes o el cRecibo tiene (0) en el idEncFactura
        If (SQL_DT.Rows(0).Item(0).ToString <> SQL_DT2.Rows(0).Item(0).ToString) Then
            If (SQL_DT.Rows(0).Item(0).ToString = 0) Then
                '-- Query 3: Valida el importe de la factura
                SQL_QUERY = "SELECT  importe-importeDestoEnv  FROM cFactura WHERE idcliente = '" & idCliente & "' AND estado = 1 AND id_encFactura  =" & idFactura
                SQL_DT3 = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
                objCe.SetExecute(SQL_QUERY)

                SQL_QUERY = "SELECT  importe-importeDestoEnv  FROM cRecibo WHERE idcliente = '" & idCliente & "' AND estado = 1 AND id_encRecibo  =" & idRecibo
                SQL_DT4 = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
                objCe.SetExecute(SQL_QUERY)

                If (SQL_DT3.Rows(0).Item(0).ToString = SQL_DT4.Rows(0).Item(0).ToString) Then
                    '--- Query 3: Incrementar el correlativo
                    SQL_QUERY = "UPDATE cRecibo  SET idEncFactura = " & idFactura & " WHERE idEncFactura = 0 AND idcliente = '" & idCliente & "' AND id_encRecibo = " & idRecibo
                    objCe.SetExecute(SQL_QUERY)
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return True
        End If

        '--- Cerrar la conexion
        Conexion.Close()

    End Function

    Public Function actualizarEstado(ByVal comRecibo As documentoCO) As String

        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        '--- Query 3: Anular recibo duplicado
        'SQL_QUERY = "UPDATE crecibo SET estado = 2, usuarioAnula=" & comRecibo.idusuario & " WHERE idcliente = " & id_glo_cliente.ToString() & " AND doTipo = '" & comRecibo.doTipo & "' AND nimpresiones = 0 AND idEncFactura = 0 "
        If (comRecibo.doTipo = "ODV") Then
            SQL_QUERY = "UPDATE crecibo SET estado = 2, usuarioAnula=" & comRecibo.idusuario & " WHERE idcliente = " & id_glo_cliente.ToString() & " AND doTipo = '" & comRecibo.doTipo & "' AND nimpresiones = 0 "
            objCe.SetExecute(SQL_QUERY)
        End If
        'SQL_QUERY = "UPDATE crecibo SET estado = 2 WHERE importe = " & comRecibo.importe & " AND idcliente = " & id_glo_cliente.ToString() & " AND doTipo = '" & comRecibo.doTipo & "' AND nimpresiones = 0 AND idEncCxC = 0  "
        If (comRecibo.doTipo = "CXC") Then
            SQL_QUERY = "UPDATE crecibo SET estado = 2 WHERE importe = " & comRecibo.importe & " AND idcliente = " & id_glo_cliente.ToString() & " AND doTipo = '" & comRecibo.doTipo & "' AND nimpresiones = 0 "
            objCe.SetExecute(SQL_QUERY)
        End If

        '--- Cerrar la conexion
        Conexion.Close()
        'MsgBox("Se duplico el recibo del cliente " & id_glo_cliente.ToString())
        '--- Devolver el id del encabezado
        Return 1

    End Function

    Public Function crearDetalle(ByVal recibo As documentoCO, ByVal linea As String) As Integer

        SQL_QUERY = _
        " INSERT INTO CRECIBO_DETALLE  " _
        & " (idEncRecibo,linea,dTipo,serie,numero,importe,saldo,estado)" _
        & " VALUES(" & recibo.idEncabezado & "," & linea & ",'" & recibo.det_dTipo & "','" & recibo.serie & "','" & recibo.numero & "'," & recibo.importePago & "," & recibo.saldo & "," & recibo.estado & ")"

        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function crearDetallePago(ByVal recibo As documentoCO)
        SQL_QUERY = _
        " INSERT INTO CRECIBO_PAGOS  " _
        & " (idEncRecibo, idViaPago, importe, documento, idInstitucion, moneda, estado) " _
        & " VALUES(" & recibo.idEncabezado & ",'" & recibo.idViaPago & "'," & recibo.importeViaPago & ",'" & recibo.documento & "'," & recibo.idInstitucion & ",'" & recibo.moneda & "'," & recibo.estado & ")"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function eliminar() As Integer

        SQL_QUERY = _
        "DELETE FROM cRecibo_detalle WHERE estado = 0 "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
       "DELETE FROM cRecibo_pagos WHERE estado = 0 "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
       "DELETE FROM cRecibo WHERE estado = 0 "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- Query 3: Decrementar el correlativo
        If SQL_RESULT <> 0 Then
            SQL_QUERY = "UPDATE rccorrelativo SET actual = actual -1 WHERE  dtipo = 'R'"
            objCe.SetExecute(SQL_QUERY)
        End If

        Return SQL_RESULT
    End Function

    Public Function getConsulta() As DataTable
        SQL_QUERY = " SELECT  D.id_encRecibo,C.id_cliente Codigo , C.negocio Cliente , D.RSerie Serie ,RNumero Numero,convert(nvarchar(10),fechaEmision,103)Femi, convert(nvarchar(10),fechaEmision,108)Hemi,D.Importe Importe,ImporteDestoEnv,ImporteDesto,DE.saldo,D.estado, doTipo,Rtrim(DE.serie) +'-'+ convert(nvarchar(20),De.numero) docPagado, nImpresiones " _
        & " FROM [CRECIBO] D" _
        & " left join CRECIBO_DETALLE  DE " _
        & " ON D.id_encRecibo = DE.idencrecibo " _
        & " left join [RCLIENTE] C " _
        & " on C.id_cliente=D.idcliente " _
        & " order by id_encRecibo desc "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getRecibo(ByVal idRecibo As String) As DataTable
        SQL_QUERY = " SELECT   R.*, C.noResolucion, C.fechaResolucion,C.inicial,C.final FROM crecibo R  " _
        & " LEFT JOIN  [RCCORRELATIVO]C on R.idruta = C.idruta  " _
        & " WHERE dTipo = 'F' AND id_encRecibo =  " & idRecibo
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getReciboByCXC(ByVal idEncCxc As String) As DataTable
        SQL_QUERY = "  " _
        & " SELECT * FROM CRECIBO WHERE idEncCxc =   '" & idEncCxc & "'" _
        & " AND estado = 1 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function VerificaRecibo(ByVal idRecibo As String) As Boolean
        Conexion = objCe.dbConnect()
        SQL_QUERY = "SELECT importe-importeDestoEnv-importeDesto FROM crecibo WHERE id_encRecibo = " + idRecibo
        SQL_DT2 = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        SQL_QUERY = " SELECT sum(importe) from crecibo_pagos WHERE  idEncRecibo =  " + idRecibo
        SQL_DT3 = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        If (SQL_DT2.Rows(0).Item(0).ToString() <> SQL_DT3.Rows(0).Item(0).ToString()) Then
            Return False
        Else
            Return True
        End If
        Conexion.Close()

    End Function

    Public Function VerificaFactura(ByVal idFactura As String, ByVal idRecibo As String) As Boolean
        Dim dif As Integer = 0
        Try
            Conexion = objCe.dbConnect()

            SQL_QUERY = "SELECT importe FROM cRecibo WHERE id_EncRecibo = " + idRecibo
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

            SQL_QUERY = "SELECT sum(importe) FROM cfactura_detalle WHERE idEncFactura = " + idFactura
            SQL_DT2 = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

            SQL_QUERY = " SELECT importe from cfactura WHERE  id_EncFactura =  " + idFactura
            SQL_DT3 = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

            If (SQL_DT2.Rows(0).Item(0).ToString() <> SQL_DT3.Rows(0).Item(0).ToString()) Then
                dif = SQL_DT2.Rows(0).Item(0).ToString() - SQL_DT2.Rows(0).Item(0).ToString()
                If (dif > -1 And dif < 1) Then
                    Return True
                Else
                    Return False
                End If

            Else
                If (SQL_DT3.Rows(0).Item(0).ToString() = SQL_DT.Rows(0).Item(0).ToString()) Then
                    Return True

                End If
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Conexion.Close()


    End Function


    Public Function VerificaFacturaFEL(ByVal idFactura As String) As Boolean
        Dim dif As Integer = 0
        Try
            Conexion = objCe.dbConnect()
            SQL_QUERY = "SELECT preimpreso FROM cFactura WHERE id_EncFactura = " + idFactura
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
            If (SQL_DT.Rows(0).Item(0).ToString() <> "") Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Conexion.Close()
    End Function

    Public Function NumeroAccesoC() As String
        Try
            Conexion = objCe.dbConnect()
            SQL_QUERY = "SELECT numeroAcceso FROM rcontingencia where usado <> 'X'"
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
            If (SQL_DT.Rows(0).Item(0).ToString() <> "X") Then
                Return SQL_DT.Rows(0).Item(0).ToString()
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
        Conexion.Close()
    End Function

    Public Function VerificaFacturaContingencia(ByVal idFactura As String) As Boolean
        Dim dif As Integer = 0
        Try
            Conexion = objCe.dbConnect()
            SQL_QUERY = "SELECT numeroacceso FROM cFactura WHERE id_EncFactura = " + idFactura
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
            If (SQL_DT.Rows(0).Item(0).ToString() <> "") Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Conexion.Close()
    End Function

    Public Function VerificaFacturaContingenciaNC(ByVal idNC As String) As Boolean
        Dim dif As Integer = 0
        Try
            Conexion = objCe.dbConnect()
            SQL_QUERY = "SELECT numeroacceso FROM cNotaCredito WHERE id_EncNC = " + idNC
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
            If (SQL_DT.Rows(0).Item(0).ToString() <> "") Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Conexion.Close()
    End Function

    Public Function VerificaFacturaFELNC(ByVal idNC As String) As Boolean
        Dim dif As Integer = 0
        Try
            Conexion = objCe.dbConnect()
            SQL_QUERY = "SELECT preimpreso FROM cNotaCredito WHERE id_EncNC = " + idNC
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
            If (SQL_DT.Rows(0).Item(0).ToString() <> "") Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Conexion.Close()
    End Function


    Public Function getReciboDetalle(ByVal idRecibo As String) As DataTable
        SQL_QUERY = " SELECT  serie, numero, importe, saldo " _
        & " FROM CRECIBO_DETALLE " _
        & " WHERE idEncRecibo  =  " & idRecibo
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getReciboPagos(ByVal idRecibo As String)
        SQL_QUERY = _
        "  SELECT  descripcion, importe,idviapago " _
        & " FROM  [CRECIBO_PAGOS] " _
        & " left join [RVIAS_PAGO] on  " _
        & " via = idviapago " _
        & " WHERE idEncRecibo =  " & idRecibo
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function setNoImpresiones(ByVal idFactura As String, ByVal numero As String) As Integer
        SQL_QUERY = " UPDATE CRECIBO " _
         & " SET nImpresiones = nImpresiones + " & numero _
        & " WHERE id_encRecibo  = " & idFactura
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function crearCxc(ByVal factura As documentoCO, ByVal Cliente As ClienteCO, ByVal recibo As documentoCO) As String

        Dim oUtil As New UtilitarioBL
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()
        Conexion.Open()

        '--- Query 0: Fecha de vencimiento de la factura
        If tipoRuta <> "16" Then
            SQL_QUERY = " UPDATE CFACTURA SET  fechaVence = GETDATE() +" & oUtil.getDataValue("CPAGO", id_glo_condicion) & "  WHERE id_encFactura = " & factura.idEncabezado
            objCe.ExecuteIdentity(SQL_QUERY, Conexion)
        End If

        '--- Query 2: insertar encabezado
        SQL_QUERY = _
        " INSERT INTO  RCXC (id_cxc,idCliente, dtipo, serie, numero, fechaEmision, fechavence, importe, saldo, importeDesto, porcentajeDesto, seriefel, numeroautorizacion) " _
        & " VALUES('" & factura.serie & factura.numero & "','" & Cliente.codigo & "','FACT','" & factura.serie & "','" & factura.numero _
        & "',GETDATE(),GETDATE() +" & Cliente.diasCredito & ",'" & factura.importe & "','" & recibo.saldo & "','" & factura.importeDestoPP & "','" & factura.porcentajeDestoPP & "','" & factura.serieFEL & "','" & factura.preimpreso & "')"
        objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Query 3: Vincular la cxc al recibo
        SQL_QUERY = " UPDATE CRECIBO SET  idEncCxc = '" & factura.serie & factura.numero & "'  WHERE id_encRecibo = " & recibo.idEncabezado
        objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Cerrar la conexion
        Conexion.Close()

        '--- Devolver el id del encabezado
        Return factura.serie & factura.numero
    End Function

    Public Function crearDetalleCxc(ByVal idcxc As String, ByVal item As ItemCO) As Integer
        Dim descuento As Decimal
        Dim objUtil As New UtilitarioBL
        descuento = objUtil.isDecimal(item.importeDestoPP) + objUtil.isDecimal(item.valorIvaDesto)
        SQL_QUERY = _
        " INSERT INTO [RCXC_DETALLE]( linea, idProducto, cajas, unidades, importe, porcentajeDesto, importeDesto, idCxc) " _
        & "VALUES ('" & item.NoItem & "','" & item.idProducto & "','" & item.cajas & "','" & item.unidades & "','" & item.importe _
        & "','" & item.porcentajeDestoPP & "','" & descuento.ToString & "','" & idcxc & "')"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function actualizaCxc(ByVal saldo As String, ByVal idCxc As String, ByVal pagoEfectivo As String) As Integer
        SQL_QUERY = _
        " UPDATE RCXC " _
        & " SET saldo = " & saldo _
        & " WHERE  id_cxc = '" & idCxc & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
       "   UPDATE RCXC " _
       & " SET pagado = " & pagoEfectivo _
       & " WHERE  id_cxc = '" & idCxc & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function actualizaidCxc(ByVal nuevoSerie As String, ByVal nuevoNumero As String, ByVal idCxc As String) As Integer
        SQL_QUERY = _
       "    UPDATE RCXC " _
       & "  SET id_cxc = '" & nuevoSerie + nuevoNumero & "'" _
       & ", SERIE = '" & nuevoSerie & "'" _
       & ", NUMERO = '" & nuevoNumero & "'" _
       & "  WHERE  id_cxc = '" & idCxc & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
     "   UPDATE RCXC_DETALLE " _
     & " SET idcxc = ' " & nuevoSerie + nuevoNumero & "'" _
     & " WHERE  idcxc = '" & idCxc & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function revertirCxc(ByVal importe As String, ByVal idCxc As String) As Integer

        '--- Revertir saldo y estado de pagado
        SQL_QUERY = _
       "   UPDATE RCXC " _
       & " SET SALDO = SALDO + " & importe _
       & ",PAGADO = 'False'" _
       & " WHERE  id_cxc = '" & idCxc & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- Si la CXC se origino en ruta entonces eliminar de la BD
        'SQL_QUERY = _
        '"   DELETE FROM  RCXC " _
        '& " WHERE  id_cxc = '" & idCxc & "' AND dtipo='FACT' "
        'SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function eliminarCxc(ByVal idCxc As String) As Integer
        SQL_QUERY = " DELETE FROM  RCXC WHERE id_cxc = '" & idCxc & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = " DELETE FROM  RCXC_DETALLE WHERE idCxc = '" & idCxc & "'"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function


    Public Function actualizar(ByVal recibo As documentoCO) As Integer
        SQL_QUERY = " UPDATE CRECIBO SET " _
       & "  [usuarioAnula]       ='" & recibo.usuarioAnula & "'" _
       & " ,[fechaAnula]        =" & recibo.fechaAnula & "" _
       & " ,[nImpresiones]      ='" & recibo.nImpresiones & "'" _
       & " ,[doTipo]            ='" & recibo.doTipo & "'" _
       & " ,[idEncFactura]      ='" & recibo.idEncFacturaRelacionada & "'" _
       & " ,[estado]      ='" & recibo.estado & "'" _
       & " WHERE id_encRecibo   = " & recibo.idEncabezado
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function actualizarDetalle(ByVal recibo As documentoCO, ByVal factura As documentoCO) As Integer
        SQL_QUERY = " UPDATE CRECIBO_DETALLE SET " _
       & "  [serie]       ='" & factura.serie & "'" _
       & " ,[numero]        =" & factura.numero & "" _
       & " WHERE idencRecibo   = " & recibo.idEncabezado
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function


    

    Public Function getRecibosPorFactura(ByVal idFactura As String) As DataTable
        SQL_QUERY = _
        " SELECT  * FROM CRECIBO " _
        & " WHERE idEncFactura = " & idFactura
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getRecibosPorCxc(ByVal idCxc As String) As DataTable
        SQL_QUERY = _
        " SELECT  * FROM CRECIBO " _
        & " WHERE idEncCxc = " & idCxc
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getPagosCredito(ByVal idRecibo As String) As DataTable
        SQL_QUERY = _
        "   SELECT  crecibo_pagos.importe  FROM CRECIBO LEFT JOIN CRECIBO_PAGOS ON idEncRecibo = id_encrecibo " _
        & " WHERE idViapago = 'CR' AND idencrecibo = " & idRecibo
        SQL_DT = objCe.GetDataSet(SQL_QUERY)

        Return SQL_DT
    End Function

    Public Function getPagosCredito(ByVal idcliente As String, ByRef rlayer As rLayerHandler) As DataTable
        SQL_QUERY = "" _
        & " SELECT SUM(saldo)saldo " _
        & " FROM " _
        & " ( " _
        & "	SELECT  SUM (saldo)saldo   FROM rcxc where idcliente = " & idcliente _
        & " )qa "
        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)
        Return SQL_DT
    End Function

    Public Function getPagosRealizadosCXC(ByVal id_cxc As String, ByRef rlayer As rLayerHandler) As DataTable
        SQL_QUERY = _
        " SELECT pago + saldo as montoTotal , pago  " _
        & " FROM  " _
        & " ( " _
        & " 	SELECT  SUM (R.importeDestoEnv + CASE when RD.importe is NULL THEN 0 ELSE RD.importe END ) as pago ,  r.idEncCxc   " _
        & " 	FROM crecibo  R LEFT JOIN crecibo_pagos RD " _
        & " 	ON id_encrecibo = idencrecibo " _
        & " 	WHERE idEncCxc = '" & id_cxc & "'" _
        & " 	GROUP BY idEncCxc " _
        & " )A " _
        & " LEFT JOIN  " _
        & " ( " _
        & " 	SELECT  saldo,  id_cxc " _
        & " 	FROM    rcxc  " _
        & " 	WHERE id_cxc = '" & id_cxc & "'" _
        & " )B " _
        & " ON A.idEncCxc = B.id_cxc "

        SQL_DT = objCe.GetDataSet(SQL_QUERY, rlayer)

        Return SQL_DT
    End Function


End Class
