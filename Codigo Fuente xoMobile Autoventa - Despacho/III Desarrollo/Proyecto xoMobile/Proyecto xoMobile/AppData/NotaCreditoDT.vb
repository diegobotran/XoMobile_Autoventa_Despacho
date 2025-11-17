Imports System.Data
Imports System.Data.SqlServerCe
Imports Proyecto_xoMobile_Packs

Public Class NotaCreditoDT
    Dim SQL_QUERY As String
    Dim SQL_QUERY2 As String
    Dim SQL_QUERY3 As String
    Dim SQL_QUERY4 As String
    Dim SQL_RESULT As Integer
    Dim SQL_RESULT2 As Integer
    Dim SQL_DT As DataTable
    Dim SQL_DT2 As DataTable
    Dim SQL_DT3 As DataTable
    Dim Conexion As SqlCeConnection
    Dim objCe As New ceClient


    '---ok
    Public Function crearEncabezado(ByVal comNotaDeCredito As documentoCO) As String
        Dim importeDesto As Decimal = 0


        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        '--- Query 1: Insertar encabezado
        importeDesto = comNotaDeCredito.importe
        importeDesto = Math.Abs(importeDesto)

        SQL_QUERY = "INSERT INTO " _
        + " cnotacredito( ncSerie, ncNumero, fechaEmision, idRuta, idcliente,importe, moneda, tTipo, tipoDevolucion, porcentajeIva,	idUsuario, estado,nImpresiones, condicion, idEncFactura,idEncRecibo )" _
        + " SELECT  serie, actual, getdate(), " + id_glo_ruta.ToString() + "," + id_glo_cliente.ToString() + "," + importeDesto.ToString() + ",'" + co_glo_moneda _
        + "','" + comNotaDeCredito.ttipo + "','" + comNotaDeCredito.tipoDevolucion + "','" + co_glo_porcentajeIVA + "','" + id_glo_usuario.ToString() _
        + "'," + comNotaDeCredito.estado + ",0, '" + id_glo_condicion + "','" & comNotaDeCredito.idEncFacturaRelacionada & "','" & comNotaDeCredito.idReciboRelacionado & "'" _
        + " FROM    rccorrelativo 	" _
        + " WHERE   dtipo = 'NC' "

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            objCe.ExecuteIdentity(SQL_QUERY, Conexion)

            '--- Query 2: Obtener el id del encabezado
            SQL_QUERY = "SELECT @@IDENTITY as nn"
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

            '--- Query 3: Incrementar el correlativo
            SQL_QUERY = "UPDATE rccorrelativo SET actual = actual + 1 WHERE  dtipo = 'NC'"
            objCe.SetExecute(SQL_QUERY)

            '--- Devolver el id del encabezado
            Return SQL_DT.Rows(0).Item(0).ToString
        Catch
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return 0
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function

    '---ok
    Public Function crearEncabezadoNA(ByVal comNotaDeCredito As documentoCO) As String
        Dim importeDesto As Decimal = 0


        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        '--- Query 1: Insertar encabezado
        importeDesto = comNotaDeCredito.importe
        importeDesto = Math.Abs(importeDesto)

        SQL_QUERY = "INSERT INTO " _
        + " cnotacredito( ncSerie, ncNumero, fechaEmision, idRuta, idcliente,importe, moneda, tTipo, tipoDevolucion, porcentajeIva,	idUsuario, estado,nImpresiones, condicion, idEncFactura,idEncRecibo, tipoDoc )" _
        + " SELECT  serie, actual, getdate(), " + id_glo_ruta.ToString() + "," + id_glo_cliente.ToString() + "," + importeDesto.ToString() + ",'" + co_glo_moneda _
        + "','" + comNotaDeCredito.ttipo + "','" + comNotaDeCredito.tipoDevolucion + "','" + co_glo_porcentajeIVA + "','" + id_glo_usuario.ToString() _
        + "'," + comNotaDeCredito.estado + ",0, '" + id_glo_condicion + "','" & comNotaDeCredito.idEncFacturaRelacionada & "','" & comNotaDeCredito.idReciboRelacionado & "','NA'" _
        + " FROM    rccorrelativo 	" _
        + " WHERE   dtipo = 'NC' "

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            objCe.ExecuteIdentity(SQL_QUERY, Conexion)

            '--- Query 2: Obtener el id del encabezado
            SQL_QUERY = "SELECT @@IDENTITY as nn"
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

            '--- Query 3: Incrementar el correlativo
            SQL_QUERY = "UPDATE rccorrelativo SET actual = actual + 1 WHERE  dtipo = 'NC'"
            objCe.SetExecute(SQL_QUERY)

            '--- Devolver el id del encabezado
            Return SQL_DT.Rows(0).Item(0).ToString
        Catch
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return 0
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function

    Public Function anularDocumentosSin()
        SQL_QUERY2 = "UPDATE cnotacredito SET estado = 2 WHERE id_encnc IN ( " _
                    + " SELECT id_encnc FROM cnotacredito " _
                    + " WHERE(estado = 1) " _
                    + " AND id_encnc NOT IN ( " _
                    + " Select a.id_encnc " _
                    + " FROM cnotacredito a, cnotacredito_detalle b  " _
                    + "   WHERE a.id_encnc = b.idEncnc " _
                    + " And a.estado = 1 " _
                    + " GROUP BY  a.id_encnc  ) )"
        SQL_RESULT2 = objCe.SetExecute(SQL_QUERY2)
        Return SQL_RESULT2
    End Function

    Public Function actualizarNC(ByVal id_detNC, ByVal idEncNC, ByVal idProducto, ByVal Importe, ByVal ImporteSinIva)
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            SQL_QUERY2 = "UPDATE cnotacredito_detalle SET importe = " & Importe & ", importeSinIva= " & ImporteSinIva & "  WHERE id_detNC =  " & id_detNC & " AND  idEncNC = " & idEncNC
            objCe.SetExecute(SQL_QUERY2)
            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return False
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function

    Public Function actualizarFacturacion(ByVal id_detFACT, ByVal idEncFACT, ByVal idProducto, ByVal Importe, ByVal ImporteSinIva)
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            SQL_QUERY2 = "UPDATE cfactura_detalle SET importe = " & Importe & ", importeSinIva= " & ImporteSinIva & "  WHERE id_detFactura =  " & id_detFACT & " AND  idEncFactura = " & idEncFACT
            objCe.SetExecute(SQL_QUERY2)
            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return False
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function

    Public Function actualizarNCImporte(ByVal idEncNC, ByVal Importe)
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            SQL_QUERY2 = "UPDATE cnotacredito SET importe = " & Importe & "  WHERE id_encNC =  " & idEncNC
            objCe.SetExecute(SQL_QUERY2)
            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return False
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function

    Public Function actualizarFACTImporte(ByVal fact, ByVal diferencia, ByVal Recibo)
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            SQL_QUERY2 = "UPDATE cFactura SET importe =  (importe) - " & (diferencia) & "  WHERE estado = 1 and id_encFactura =  " & fact
            objCe.SetExecute(SQL_QUERY2)

            SQL_QUERY3 = "UPDATE cRecibo SET importe =  importe - " & (diferencia) & " WHERE id_EncRecibo =  " & Recibo
            objCe.SetExecute(SQL_QUERY3)

            Return True
        Catch ex As Exception
            'MsgBox("Error al crear la nota de credito" + ex.Message())
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error ajuste factura")
            Return False
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function


    Public Function actualizarReciboPagosImporte(ByVal Recibo, ByVal diferencia, ByVal ViaPago)
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()
        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            SQL_QUERY2 = "UPDATE cRecibo_pagos SET importe =  (importe) - " & (diferencia) & "  WHERE idEncRecibo = " & Recibo & " and idViaPago = '" & ViaPago & "'"
            objCe.SetExecute(SQL_QUERY2)
            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return False
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function

    Public Function actualizarReciboPagosImporte2(ByVal Recibo, ByVal diferencia, ByVal ViaPago)
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()
        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            SQL_QUERY2 = "UPDATE cRecibo_pagos SET importe =  (importe)  + " & (diferencia) & "  WHERE idEncRecibo = " & Recibo & " and idViaPago = '" & ViaPago & "'"
            objCe.SetExecute(SQL_QUERY2)
            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return False
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function


    Public Function actualizarRECImporte(ByVal fact, ByVal Importe)
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            SQL_QUERY2 = "UPDATE cRecibo SET importe = " & Importe & "  WHERE idEncFactura =  " & fact
            objCe.SetExecute(SQL_QUERY2)
            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return False
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function

    Public Function actualizarRECIBOImporte(ByVal fact, ByVal Importe)
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            SQL_QUERY2 = "UPDATE cRecibo SET importe = " & Importe & "  WHERE idEncFactura =  " & fact
            objCe.SetExecute(SQL_QUERY2)
            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return False
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function


    Public Function actualizarFACTImporteEnvase(ByVal Facturas, ByVal ImporteEnvase)
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            SQL_QUERY2 = "UPDATE cfactura SET ImporteDestoEnv = " & ImporteEnvase & "  WHERE id_encFactura =  " & Facturas
            objCe.SetExecute(SQL_QUERY2)

            

            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return False
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function

    Public Function obtenerDiferencias(ByVal Facturas, ByVal vBruto)
        Dim importeRecibo As Decimal = 0
        Dim importeRecibo_pagos As Decimal = 0
        Dim idRecibo As Integer = 0
        Dim Resultado As Decimal = 0

        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try

            

            SQL_QUERY4 = "SELECT idEncRecibo FROM cfactura WHERE id_encFactura =  " & Facturas
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY4, Conexion)
            idRecibo = SQL_DT.Rows(0).Item(0).ToString()

            SQL_QUERY = "UPDATE cRecibo SET ImporteDestoEnv = " & vBruto & "  WHERE id_encRecibo =  " & idRecibo
            objCe.SetExecute(SQL_QUERY)

            SQL_QUERY2 = "SELECT importe-importeDesto-importeDestoEnv FROM cRecibo WHERE id_EncRecibo =  " & idRecibo
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY2, Conexion)
            importeRecibo = SQL_DT.Rows(0).Item(0).ToString()


            SQL_QUERY3 = "SELECT sum(importe) FROM cRecibo_pagos WHERE idEncRecibo =  " & idRecibo
            SQL_DT2 = objCe.ExecuteIdentity(SQL_QUERY3, Conexion)
            importeRecibo_pagos = SQL_DT2.Rows(0).Item(0).ToString()

            Resultado = importeRecibo - importeRecibo_pagos

            Return Resultado
        Catch ex As Exception
            Return 0
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function

    Public Function obtenerRecibo_factura(ByVal Factura)
        Dim idRecibo As Integer = 0
        Conexion = objCe.dbConnect()

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try



            SQL_QUERY4 = "SELECT idEncRecibo FROM cfactura WHERE id_encFactura =  " & Factura
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY4, Conexion)
            idRecibo = SQL_DT.Rows(0).Item(0).ToString()
            Return idRecibo
        Catch ex As Exception
            Return 0
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function


    Public Function actualizarRECIBOImporteEnvase(ByVal Recibo, ByVal ImporteEnvase)
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            SQL_QUERY2 = "UPDATE cRecibo SET ImporteDestoEnv = " & ImporteEnvase & "  WHERE id_encRecibo =  " & Recibo
            objCe.SetExecute(SQL_QUERY2)

            Return True
        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return False
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try
    End Function


    '---ok version 3.3
    Public Function actualizar(ByVal documento As String) As String
        Dim importeDesto As Decimal = 0
        Dim encFactura As Integer = 0
        Dim encRecibo As Integer = 0
        Dim totalRec As Decimal = 0
        Dim importeDestoEnv As Decimal = 0
        Dim RcalculoRecibo As Decimal = 0
        Dim importeDetalle As Decimal = 0
        Dim id_clientes

        Dim objDocumento As New DocumentoBL
        Dim objDocumentoBL As New DocumentoBL
        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        Conexion.Open()
        'Dim tx As SqlCeTransaction = Conexion.BeginTransaction
        Try
            '--- Query 1: Obtener el id del encabezado
            SQL_QUERY = "SELECT sum(dt.importe), nc.idEncFactura, nc.idEncRecibo, nc.idcliente FROM cnotacredito_detalle dt, cnotacredito nc  WHERE nc.id_encNC = dt.idEncNC AND  dt.idEncNc = " + documento + " GROUP BY nc.idEncFactura, nc.idEncRecibo, nc.idCliente"
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

            importeDestoEnv = SQL_DT.Rows(0).Item(0).ToString()
            encFactura = SQL_DT.Rows(0).Item(1).ToString()
            encRecibo = SQL_DT.Rows(0).Item(2).ToString()
            id_clientes = SQL_DT.Rows(0).Item(3).ToString()

            'MessageBox.Show("El idEncFactura es : " + encFactura + "\n" + " El idEncRecibo es : " + encRecibo)

            '--- Query 2: 
            SQL_QUERY2 = "UPDATE cnotacredito SET importe = " + SQL_DT.Rows(0).Item(0).ToString() + " WHERE id_encNC =  " + documento
            objCe.SetExecute(SQL_QUERY2)
            'Esto se actualizo debido a que esta descuadrando el importe de las notas de credito en el recibo y la factura.
            '--- Query 3: Actualización ImporteDestoEnv siempre y cuando sea diferente de 0 el idEncRecibo
            If (encFactura <> 0) Then
                SQL_QUERY3 = "UPDATE cfactura SET importeDestoEnv = " + SQL_DT.Rows(0).Item(0).ToString() + " WHERE id_encFactura =  " + SQL_DT.Rows(0).Item(1).ToString()
                objCe.SetExecute(SQL_QUERY3)
            End If

            '--- Query 4: Actualización ImporteDestoEnv siempre y cuando sea diferente de 0 el idEncFactura
            If (encRecibo <> 0) Then
                SQL_QUERY4 = "UPDATE crecibo SET importeDestoEnv = " + SQL_DT.Rows(0).Item(0).ToString() + " WHERE id_encRecibo =  " + SQL_DT.Rows(0).Item(2).ToString()
                objCe.SetExecute(SQL_QUERY4)
            End If

            '-- Verificación cuadre facturación
            Return SQL_DT.Rows(0).Item(0).ToString()

        Catch ex As Exception
            MsgBox("Error al crear la nota de credito" + ex.Message())
            'tx.Rollback()
            MsgBox("Favor de revisar que se genero un error en la nota de credito")
            Return 0
        Finally
            '--- Cerrar la conexion
            Conexion.Close()
        End Try


    End Function

    Public Function agregarItem(ByVal item As ItemCO) As Integer
        If item.porcentajeDesto = "" Then
            item.porcentajeDesto = "0"
        End If

        SQL_QUERY = "INSERT INTO CNOTACREDITO_DETALLE " _
        + " (idEncNc,item, idProducto, um, cantidad, precio,importeSinIva, importe, iva, " _
        + " porcentajeDesto, importeDesto,idRubro, trqt, litm, estado) " _
        + " values(" + item.idEncabezado + "," + item.NoItem + "," + item.idProducto + ",'" + item.um + "'," + item.cantidad + "," + item.precio + "," + item.importeSinIva + "," + item.importe + "," + item.iva _
        + " ,'" + item.porcentajeDesto + "'," + item.importeDesto.ToString + ",'" + item.idRubro + "','" + item.trqt + "'," + item.litm + "," + item.estado + ")"
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    'Public Function getResumen(ByVal masterId As String) As DataTable
    '    SQL_QUERY = _
    '    " select " _
    '    + " sum(importe)		    as importe			, " _
    '    + " sum(importeDesto)    as importeDesto		, " _
    '    + " sum(importeDestoPP)	as importeDestoPP     " _
    '    + " from CFACTURA_DETALLE " _
    '    + " WHERE idEncFactura = 	" + masterId
    '    SQL_DT = objCe.GetDataSet(SQL_QUERY)
    '    Return SQL_DT
    'End Function

    Public Function eliminar() As Integer

        SQL_QUERY = _
        "DELETE FROM cnotacredito_detalle WHERE estado = 0 "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        SQL_QUERY = _
       "DELETE FROM cnotacredito WHERE estado = 0 "
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)

        '--- Query 3: Decrementar el correlativo
        If SQL_RESULT <> 0 Then
            SQL_QUERY = "UPDATE rccorrelativo SET actual = actual -1 WHERE  dtipo = 'NC'"
            objCe.SetExecute(SQL_QUERY)
        End If
        Return SQL_RESULT
    End Function

    'Public Function eliminarDetalle() As Integer
    '    SQL_QUERY = _
    '"DELETE FROM cnotacredito_DETALLE WHERE estado = 0 "
    '    SQL_RESULT = objCe.SetExecute(SQL_QUERY)
    '    Return SQL_RESULT
    'End Function

    Public Function vincularRecibo(ByVal idRecibo As String, ByVal idNotaDeCredito As String) As Integer
        SQL_QUERY = _
        "UPDATE cnotacredito set idEncRecibo = " + idRecibo + "WHERE id_encNc = " + idNotaDeCredito
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    'Public Function vincularOrdenVenta(ByVal idOrden As String, ByVal idNotaDeCredito As String) As Integer
    '    SQL_QUERY = _
    '    "UPDATE cnotacredito set idEncFactura = " + idOrden + "WHERE id_encNc = " + idNotaDeCredito
    '    SQL_RESULT = objCe.SetExecute(SQL_QUERY)
    '    Return SQL_RESULT
    'End Function

    Public Function getConsulta() As DataTable
        SQL_QUERY = _
        " select D.id_encNc, C.id_cliente Codigo , C.negocio Cliente , D.ncSerie Serie, D.tipoDevolucion ,ncNumero Numero,D.serie seriefel, D.numeroautorizacion, D.preimpreso, T.showvalue Tipo, convert(nvarchar(10),fechaEmision,103) Femi, convert(nvarchar(10),fechaEmision,108)Hemi,Importe Importe,estado,nImpresiones,idEncFactura,idEncRecibo, numeroacceso " _
        + " from [CNOTACREDITO] D  " _
        + " left join [RCLIENTE] C " _
        + " on C.id_cliente=D.idcliente " _
        + " LEFT JOIN TTIPO T ON dataValue = ttipo " _
        + " where tabla = 'NOTA_CREDITO' and tipodoc is null " _
        & "Order By id_encNc Desc"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getConsultaNA() As DataTable
        SQL_QUERY = _
        " select D.id_encNc, C.id_cliente Codigo , C.negocio Cliente , D.ncSerie Serie ,ncNumero Numero,D.serie seriefel, D.numeroautorizacion, D.preimpreso, T.showvalue Tipo, convert(nvarchar(10),fechaEmision,103) Femi, convert(nvarchar(10),fechaEmision,108)Hemi,Importe Importe,estado,nImpresiones,idEncFactura,idEncRecibo, numeroacceso " _
        + " from [CNOTACREDITO] D  " _
        + " left join [RCLIENTE] C " _
        + " on C.id_cliente=D.idcliente " _
        + " LEFT JOIN TTIPO T ON dataValue = ttipo " _
        + " where tabla = 'NOTA_CREDITO' and tipodoc = 'NA' " _
        & "Order By id_encNc Desc"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getConsultaResumen() As DataTable
        SQL_QUERY = _
         " select  rp.idMarca, tt.showValue desc_marca, sum(rp.litrosUnidad * cf.cantidad) Total_litros, sum(cf.importeSinIva) importeSinIva " _
         + " from cfactura_detalle cf, rproducto rp, ttipo tt, cfactura fac" _
         + " where cf.idProducto = rp.id_producto  " _
         + " and tt.tabla = rp.idMarca " _
         + " and fac.id_encFactura = cf.idEncFactura " _
         + " and fac.ttipo <> 'CD' " _
         + " and cf.idRubro = 'L' " _
         + " and fac.estado = 1 " _
         + " GROUP BY rp.idMarca, tt.showValue "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getNotaDeCredito(ByVal idNotaCredito As String) As DataTable
        SQL_QUERY = " SELECT  R.*, C.noResolucion, C.fechaResolucion,C.inicial,C.final  " _
        + " FROM cnotacredito R   " _
        + " LEFT JOIN  [RCCORRELATIVO]C on R.idruta = C.idruta  " _
        + " WHERE dTipo = 'NC' AND id_encNc = " + idNotaCredito
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getNotaDeCreditoDetalle(ByVal idNotaCredito As String) As DataTable
        SQL_QUERY = " SELECT	NC.* , P.descripcion  " _
        + " FROM 	CNOTACREDITO_DETALLE NC " _
        + " LEFT JOIN RPRODUCTO     P  " _
        + " ON NC.litm      =   P.id_producto  " _
        + " WHERE 	idEncNc  = " + idNotaCredito
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getNotaDeCreditoDetalle2(ByVal idEncFact As String) As DataTable
        SQL_QUERY = " SELECT NC.*, P.descripcion FROM cnotacredito_detalle NC  " _
        + " Inner join cnotacredito CN on NC.idEncNc = CN.id_encNC " _
        + " left JOIN RPRODUCTO P  ON NC.litm = p.id_producto  " _
        + " WHERE 	CN.id_encNC  = " + idEncFact
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function setNoImpresiones(ByVal idFactura As String, ByVal numero As String) As Integer
        SQL_QUERY = " UPDATE cnotacredito " _
         + " SET nImpresiones = nImpresiones + " + numero _
        + " WHERE id_encNc  = " + idFactura
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function getNotaCreditoPorRecibo(ByVal idRecibo As String) As DataTable
        SQL_QUERY = _
        "   SELECT * FROM CNOTACREDITO " _
        & " WHERE idEncRecibo = " + idRecibo _
        & " AND ESTADO =1 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getNotaCreditoPorFactura(ByVal idFactura As String) As DataTable
        SQL_QUERY = _
        "   SELECT * FROM CNOTACREDITO " _
        & " WHERE idEncFactura = " + idFactura _
        & " AND ESTADO =1 "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function actualizar(ByVal NotaCredito As documentoCO) As Integer
        SQL_QUERY = " UPDATE CNOTACREDITO SET " _
        & "  [usuarioAnula]      = '" + NotaCredito.usuarioAnula + "'" _
        & " ,[fechaAnula]        = " + NotaCredito.fechaAnula + "" _
        & " ,[nImpresiones]      = '" + NotaCredito.nImpresiones + "'" _
        & " ,[doTipo]            = '" + NotaCredito.doTipo + "'" _
        & " ,[estado]            = '" + NotaCredito.estado + "'" _
        & " ,[ncserie]           = '" & NotaCredito.serie & "'" _
        & " ,[ncnumero]          = '" & NotaCredito.numero & "'" _
        & " ,[idEncFactura]      = '" & NotaCredito.idEncFacturaRelacionada & "'" _
        & "   WHERE id_encNc     = " & NotaCredito.idEncabezado
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function actualizarDetalle(ByVal idFacturaOriginal As String, ByVal idFacturaCopia As String) As Integer

        SQL_QUERY = " UPDATE CNOTACREDITO_DETALLE SET " _
               & "  [idEncNc]      = '" & idFacturaCopia & "'" _
               & "  WHERE idencnc =  " & idFacturaOriginal
        SQL_RESULT = objCe.SetExecute(SQL_QUERY)
        Return SQL_RESULT
    End Function

    Public Function setCopiaNC(ByVal idNC As String) As Integer

        '--- Crear una conexion para esta transaccion
        Conexion = objCe.dbConnect()

        SQL_QUERY = "  " _
               & " INSERT INTO [CNOTACREDITO]([idRuta],[ncSerie],[ncNumero],[fechaEmision],[idcliente],[importe],[moneda],[tTipo],[tipoDevolucion],[usuarioAnula],[fechaAnula],[nImpresiones],[idEncFactura],[idEncRecibo],[porcentajeIva],[idUsuario],[doTipo],[estado],[condicion]) " _
               & " select [idRuta],[ncSerie],[ncNumero],[fechaEmision],[idcliente],[importe],[moneda],[tTipo],[tipoDevolucion],[usuarioAnula],[fechaAnula],[nImpresiones],[idEncFactura],[idEncRecibo],[porcentajeIva],[idUsuario],[doTipo],[estado],[condicion]" _
               & " from cnotacredito where id_encnc = " & idNC
        Conexion.Open()
        objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Query 2: Obtener el id del encabezado
        SQL_QUERY = "SELECT @@IDENTITY as nn"
        SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)

        '--- Cerrar la conexion
        Conexion.Close()

        '--- Devolver el id del encabezado
        Return SQL_DT.Rows(0).Item(0).ToString
        Return SQL_RESULT
    End Function


    '-------------FUNCIONES AGREGADAS PARA ANULAR NC DIRECTAMENTE 
    ' En NotaCreditoDT.vb
    Public Function getDocumentoPadreEstado(ByVal idNc As String) As DataTable
        Dim SQL_QUERY As String
        SQL_QUERY = "  " _
                & " SELECT nc.idEncFactura, nc.idEncRecibo, f.estado AS estadoFactura, r.estado AS estadoRecibo " _
                & " FROM CNOTACREDITO nc " _
                & " LEFT JOIN CFACTURA f ON nc.idEncFactura = f.id_encFactura " _
                & " LEFT JOIN CRECIBO r ON nc.idEncRecibo = r.id_encRecibo " _
                & " WHERE nc.id_EncNc = " & idNc
        Return objCe.GetDataSet(SQL_QUERY)
    End Function

    ' --- Nueva función centralizada ---
    Public Function anularNotaCreditoBD(ByVal idNc As String, ByVal usuario As String, ByVal motivo As String) As Integer
        Dim SQL_QUERY As String
        SQL_QUERY = " " _
                & " UPDATE CNOTACREDITO " _
                & "SET estado = 2, " _
                & "usuarioAnula = '" & usuario & "', " _
                & "fechaAnula = GETDATE(), " _
                & "WHERE id_EncNc = " & idNc
        Return objCe.SetExecute(SQL_QUERY)
    End Function



End Class
