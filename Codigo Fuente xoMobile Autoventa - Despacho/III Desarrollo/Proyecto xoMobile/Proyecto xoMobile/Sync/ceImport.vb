Imports System.Data.SqlServerCe
Imports System.Data


Public Class ceImport

    Dim objUtil As New UtilitarioBL
    Dim oceClient As New ceClient

#Region " IMPORTAR ASIGNACION DE VIAS DE PAGO "
    Public Function importIntoRASIGNA_VIAS_PAGO(ByVal RASIGNA_VIAS_PAGO As DataTable, ByRef dtImportLog As DataTable, ByVal isCr As Boolean) As Boolean

        Dim friendlyFunction As String
        Dim dsRASIGNA_VIAS_PAGO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RASIGNA_VIAS_PAGO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRASIGNA_VIAS_PAGO, "RASIGNA_VIAS_PAGO")

        'Insertar datos
        For i As Integer = 0 To RASIGNA_VIAS_PAGO.Rows.Count - 1
            Dim ceRow As DataRow = dsRASIGNA_VIAS_PAGO.Tables("RASIGNA_VIAS_PAGO").NewRow()
            ceRow("idCliente") = RASIGNA_VIAS_PAGO.Rows(i).Item("KUNNR")
            ceRow("idViaPago") = RASIGNA_VIAS_PAGO.Rows(i).Item("ZWELS")
            dsRASIGNA_VIAS_PAGO.Tables("RASIGNA_VIAS_PAGO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRASIGNA_VIAS_PAGO, "RASIGNA_VIAS_PAGO")
        oceClient.Conexion.Close()
        If isCr Then
            friendlyFunction = "Clientes con derecho a credito"
        Else
            friendlyFunction = "Asignacion de vias de pago"
        End If
        If RASIGNA_VIAS_PAGO.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", friendlyFunction, "", "importIntoRASIGNA_VIAS_PAGO", RASIGNA_VIAS_PAGO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return False
        End If
        dtImportLog.Rows.Add(New String() {"Exito", friendlyFunction, "", "importIntoRASIGNA_VIAS_PAGO", RASIGNA_VIAS_PAGO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR BOM "
    Public Function importIntoBOM(ByVal BOOM As DataTable, ByRef dtImportLog As DataTable) As Boolean

        '--- Sentencia por SQL command
        Dim delcmd As SqlCeCommand
        Dim dsBOOM As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from BOOM", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsBOOM, "BOOM")
        delcmd = New SqlCeCommand("DELETE FROM BOOM", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To BOOM.Rows.Count - 1
            Dim ceRow As DataRow = dsBOOM.Tables("BOOM").NewRow()
            ceRow("idProducto") = BOOM.Rows(i).Item("MATNR")
            ceRow("linea") = BOOM.Rows(i).Item("STLKN")
            ceRow("idexplosion") = BOOM.Rows(i).Item("IDNRK")
            dsBOOM.Tables("BOOM").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsBOOM, "BOOM")
        oceClient.Conexion.Close()

        If BOOM.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Bom", "", "importIntoBOM", BOOM.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Bom", "", "importIntoBOM", BOOM.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR CARGA DEL CAMION "
    Public Function importIntoRCARGA(ByVal RCARGA_LOG As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim delcmd As SqlCeCommand
        Dim dsRCARGA_LOG As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RCARGA_LOG", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRCARGA_LOG, "RCARGA_LOG")
        delcmd = New SqlCeCommand("DELETE FROM RCARGA_LOG ", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RCARGA_LOG.Rows.Count - 1
            Dim ceRow As DataRow = dsRCARGA_LOG.Tables("RCARGA_LOG").NewRow()
            ceRow("sd") = 1
            ceRow("idRuta") = id_glo_ruta
            ceRow("idProducto") = RCARGA_LOG.Rows(i).Item("MATNR")
            ceRow("um") = RCARGA_LOG.Rows(i).Item("UNIDAD_MEDIDA")
            ceRow("cantidadInicial") = objUtil.isDecimal(RCARGA_LOG.Rows(i).Item("KULAB"))
            ceRow("cantidadActual") = objUtil.isDecimal(RCARGA_LOG.Rows(i).Item("KULAB"))
            ceRow("control") = 1
            ceRow("fechaCreacion") = RCARGA_LOG.Rows(i).Item("FECHA_CREACION")
            ceRow("horaCreacion") = RCARGA_LOG.Rows(i).Item("HORA_CREACION")
            ceRow("confirmada") = "false"
            dsRCARGA_LOG.Tables("RCARGA_LOG").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRCARGA_LOG, "RCARGA_LOG")
        oceClient.Conexion.Close()

        '--- Validar el parametro de carga cuando la ruta no valida el inventario
        If RCARGA_LOG.Rows.Count = 0 And xo_validaInventario Then
            dtImportLog.Rows.Add(New String() {"Error", "Carga de producto", "", "importIntoRCARGA_LOG", RCARGA_LOG.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return False
        End If

        If Not xo_validaInventario Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Carga de producto", "", "importIntoRCARGA_LOG", RCARGA_LOG.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Carga de producto", "", "importIntoRCARGA_LOG", RCARGA_LOG.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR CLUSTER SEGMENTACION "
    Public Function importIntoRCLUSTER_SEG(ByVal RCLUSTER_SEG As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim dsRCLUSTER_SEG As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RCLUSTER_SEG", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRCLUSTER_SEG, "RCLUSTER_SEG")

        'Insertar datos
        For i As Integer = 0 To RCLUSTER_SEG.Rows.Count - 1
            Dim ceRow As DataRow = dsRCLUSTER_SEG.Tables("RCLUSTER_SEG").NewRow()
            ceRow("sociedad") = RCLUSTER_SEG.Rows(i).Item("BUKRS")
            ceRow("idOficinaVentas") = RCLUSTER_SEG.Rows(i).Item("VKBUR").ToString
            ceRow("idDepartamento") = RCLUSTER_SEG.Rows(i).Item("REGIO").ToString
            ceRow("idClaseCliente") = RCLUSTER_SEG.Rows(i).Item("KUKLA").ToString
            ceRow("idRegion") = RCLUSTER_SEG.Rows(i).Item("BRAN2").ToString
            ceRow("idCanal") = RCLUSTER_SEG.Rows(i).Item("BRAN3").ToString
            ceRow("idTipoRuta") = RCLUSTER_SEG.Rows(i).Item("BRAN4").ToString
            ceRow("idMarca") = RCLUSTER_SEG.Rows(i).Item("mvgr2").ToString
            ceRow("desMarca") = RCLUSTER_SEG.Rows(i).Item("bezei").ToString
            ceRow("idProducto") = objUtil.isDecimal(RCLUSTER_SEG.Rows(i).Item("MATNR").ToString)
            ceRow("tipoDato") = RCLUSTER_SEG.Rows(i).Item("TIPODATO").ToString
            ceRow("tipo") = RCLUSTER_SEG.Rows(i).Item("ZTIPO_PEDMIN").ToString
            ceRow("accion") = RCLUSTER_SEG.Rows(i).Item("ZACC_PEDMIN").ToString
            ceRow("valorMinimo") = RCLUSTER_SEG.Rows(i).Item("VALOR").ToString
            ceRow("unidadMedida") = RCLUSTER_SEG.Rows(i).Item("DIMENSION").ToString
            ceRow("giro") = RCLUSTER_SEG.Rows(i).Item("BRSCH").ToString
            dsRCLUSTER_SEG.Tables("RCLUSTER_SEG").Rows.Add(ceRow)
        Next

        ceDataAdapter.Update(dsRCLUSTER_SEG, "RCLUSTER_SEG")
        oceClient.Conexion.Close()
        If RCLUSTER_SEG.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Cluster Segmentacion", "", "importIntoRCLUSTER_SEG", RCLUSTER_SEG.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Cluster Segmentacion", "", "importIntoRCLUSTER_SEG", RCLUSTER_SEG.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True
    End Function
#End Region

#Region " IMPORTAR CANDADOS DE MATERIAL "
    Public Function importIntoRLockMaterial(ByVal RLOCKMATERIAL As DataTable, ByRef dtImportLog As DataTable) As Boolean



        Dim delcmd As SqlCeCommand
        Dim dsRCLIENTE As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RLOCK_MATERIAL", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRCLIENTE, "RLOCK_MATERIAL")
        delcmd = New SqlCeCommand("DELETE FROM RLOCK_MATERIAL", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RLOCKMATERIAL.Rows.Count - 1
            Dim ceRow As DataRow = dsRCLIENTE.Tables("RLOCK_MATERIAL").NewRow()
            ceRow("sociedad") = RLOCKMATERIAL.Rows(i).Item("BUKRS").ToString
            ceRow("idProducto") = RLOCKMATERIAL.Rows(i).Item("MATNR").ToString
            ceRow("ramo") = RLOCKMATERIAL.Rows(i).Item("BRSCH").ToString
            ceRow("ruta") = RLOCKMATERIAL.Rows(i).Item("ROUTE").ToString
            ceRow("grupoVentas") = RLOCKMATERIAL.Rows(i).Item("VKGRP").ToString
            ceRow("region") = RLOCKMATERIAL.Rows(i).Item("REGIO").ToString
            ceRow("categoria") = RLOCKMATERIAL.Rows(i).Item("KONDA").ToString
            ceRow("idCliente") = RLOCKMATERIAL.Rows(i).Item("KUNNR").ToString
            ceRow("ramo5") = RLOCKMATERIAL.Rows(i).Item("BRAN5").ToString
            dsRCLIENTE.Tables("RLOCK_MATERIAL").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRCLIENTE, "RLOCK_MATERIAL")
        oceClient.Conexion.Close()
        If RLOCKMATERIAL.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Candado de material", "", "importIntoRLOCKMATERIAL", RLOCKMATERIAL.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Candado de material", "", "importIntoRLOCKMATERIAL", RLOCKMATERIAL.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region

#Region " IMPORTAR CREDENCIALES "
    Public Function importIntoRCREDENCIALES(ByVal RCREDENCIALES As DataTable, ByRef dtImportLog As DataTable) As Boolean

        '--- Sentencia por SQL command
        Dim delcmd As SqlCeCommand
        Dim dsRCREDENCIALES As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RCREDENCIALES", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRCREDENCIALES, "RCREDENCIALES")
        delcmd = New SqlCeCommand("DELETE FROM RCREDENCIALES", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RCREDENCIALES.Rows.Count - 1
            Dim ceRow As DataRow = dsRCREDENCIALES.Tables("RCREDENCIALES").NewRow()
            ceRow("USUARIO") = RCREDENCIALES.Rows(i).Item("USUARIO")
            ceRow("PASSWORDS") = RCREDENCIALES.Rows(i).Item("PASSWORD")
            ceRow("ESTABLEC") = RCREDENCIALES.Rows(i).Item("ESTABLEC")
            ceRow("TIPO_RESP") = RCREDENCIALES.Rows(i).Item("TIPO_RESP")
            'ceRow("NIT") = RCREDENCIALES.Rows(i).Item("NIT")
            ceRow("NIT") = RCREDENCIALES.Rows(i).Item("NIT")
            ceRow("SERVIDOR") = RCREDENCIALES.Rows(i).Item("SERVIDOR")
            ceRow("SERVICIO") = RCREDENCIALES.Rows(i).Item("SERVICIO")
            ceRow("PROTOCOLO") = RCREDENCIALES.Rows(i).Item("PROTOCOLO")
            ceRow("INTERNET") = RCREDENCIALES.Rows(i).Item("INTERNET")
            dsRCREDENCIALES.Tables("RCREDENCIALES").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRCREDENCIALES, "RCREDENCIALES")
        oceClient.Conexion.Close()

        If RCREDENCIALES.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Credenciales", "", "importIntoRCREDENCIALES", RCREDENCIALES.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Credenciales", "", "importIntoRCREDENCIALES", RCREDENCIALES.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region

#Region " IMPORTAR CLIENTES "
    Public Function importIntoRCLIENTE(ByVal RCLIENTE As DataTable, ByRef dtImportLog As DataTable) As Boolean

        '--- Asignacion de vias de pago
        Dim dtAsignaViasPago As New DataTable
        dtAsignaViasPago.TableName = "RASIGNA_VIAS_PAGO"
        dtAsignaViasPago.Columns.Add("KUNNR", String.Empty.GetType())
        dtAsignaViasPago.Columns.Add("ZWELS", String.Empty.GetType())

        Dim delcmd As SqlCeCommand
        Dim dsRCLIENTE As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RCLIENTE", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRCLIENTE, "RCLIENTE")
        delcmd = New SqlCeCommand("DELETE FROM RCLIENTE", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RCLIENTE.Rows.Count - 1
            Dim ceRow As DataRow = dsRCLIENTE.Tables("RCLIENTE").NewRow()
            ceRow("id_cliente") = objUtil.isInteger(RCLIENTE.Rows(i).Item("KUNNR"))
            ceRow("diaVisita") = objUtil.isInteger(RCLIENTE.Rows(i).Item("KATR6"))
            ceRow("orden") = objUtil.isInteger(RCLIENTE.Rows(i).Item("KATR8"))
            ceRow("idRuta") = id_glo_ruta
            ceRow("sociedad") = RCLIENTE.Rows(i).Item("BUKRS")
            ceRow("ramo") = RCLIENTE.Rows(i).Item("BRSCH")
            ceRow("ruta") = RCLIENTE.Rows(i).Item("SORTL")
            ceRow("grupoVentas") = RCLIENTE.Rows(i).Item("VKGRP")
            ceRow("region") = RCLIENTE.Rows(i).Item("REGIO")
            ceRow("negocio") = RCLIENTE.Rows(i).Item("NAME1")
            ceRow("propietario") = RCLIENTE.Rows(i).Item("NAME4")
            ceRow("nit") = RCLIENTE.Rows(i).Item("STCD1")
            ceRow("numeroDi") = RCLIENTE.Rows(i).Item("STCD5")
            ceRow("direccion") = RCLIENTE.Rows(i).Item("TEXT100")
            ceRow("telefono") = RCLIENTE.Rows(i).Item("TELF1")
            ceRow("categoria") = RCLIENTE.Rows(i).Item("KONDA")
            ceRow("creditoAutorizado") = RCLIENTE.Rows(i).Item("KLIMK")
            ceRow("creditoDisponible") = objUtil.isDecimal(RCLIENTE.Rows(i).Item("SAUFT"))
            ceRow("diasCredito") = objUtil.isInteger(RCLIENTE.Rows(i).Item("DZTAGE"))
            ceRow("volPresupuesto") = RCLIENTE.Rows(i).Item("LIT_PRESUPUESTO")
            ceRow("ventaConSaldo") = RCLIENTE.Rows(i).Item("KATR1")
            ceRow("ventaConSaldov") = RCLIENTE.Rows(i).Item("KATR2")
            ceRow("nFaltas") = objUtil.isInteger(RCLIENTE.Rows(i).Item("KATR9"))
            ceRow("visitado") = 0
            ceRow("condicion") = RCLIENTE.Rows(i).Item("ZTERM")
            ceRow("listaPrecio") = RCLIENTE.Rows(i).Item("PLTYP")
            ceRow("oficinaVentas") = RCLIENTE.Rows(i).Item("VKBUR")
            ceRow("departamento") = RCLIENTE.Rows(i).Item("REGIO")
            ceRow("claseCliente") = RCLIENTE.Rows(i).Item("KUKLA")
            ceRow("region_s") = RCLIENTE.Rows(i).Item("BRAN2")
            ceRow("canal") = RCLIENTE.Rows(i).Item("BRAN3")
            ceRow("tipoRuta") = RCLIENTE.Rows(i).Item("BRAN4")
            ceRow("frecuencia") = objUtil.isInteger(RCLIENTE.Rows(i).Item("KATR7"))
            ceRow("vale_bon") = RCLIENTE.Rows(i).Item("VALE_BON")
            ceRow("reclamo") = RCLIENTE.Rows(i).Item("RECLAMO")
            ceRow("devolucion") = RCLIENTE.Rows(i).Item("DEVOLUCION")
            ceRow("ramo5") = RCLIENTE.Rows(i).Item("BRAN5")

            If RCLIENTE.Rows(i).Item("KATR5") = "CR" And objUtil.isDecimal(RCLIENTE.Rows(i).Item("KLIMK")) <> 0 Then
                dtAsignaViasPago.Rows.Add(New String() {objUtil.isInteger(RCLIENTE.Rows(i).Item("KUNNR")).ToString(), "CR"})
            End If
            dsRCLIENTE.Tables("RCLIENTE").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRCLIENTE, "RCLIENTE")
        oceClient.Conexion.Close()
        importIntoRASIGNA_VIAS_PAGO(dtAsignaViasPago, dtImportLog, True)
        If RCLIENTE.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Clientes", "", "importIntoRCLIENTE", RCLIENTE.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Clientes", "", "importIntoRCLIENTE", RCLIENTE.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR CORRELATIVOS SAP "
    Public Function importIntoCorrelativoSAP(ByVal RCCORRELATIVO As DataTable, ByRef dtImportLog As DataTable) As Boolean
        Dim dsRCCORRELATIVO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RCCORRELATIVO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRCCORRELATIVO, "RCCORRELATIVO")
        'Insertar datos
        For i As Integer = 0 To RCCORRELATIVO.Rows.Count - 1
            Dim ceRow As DataRow = dsRCCORRELATIVO.Tables("RCCORRELATIVO").NewRow()
            ceRow("idRuta") = id_glo_ruta
            ceRow("dTipo") = RCCORRELATIVO.Rows(i).Item("MSCOUNTER")
            ceRow("noResolucion") = RCCORRELATIVO.Rows(i).Item("TEXT30")
            ceRow("fechaResolucion") = RCCORRELATIVO.Rows(i).Item("ARCH_DATS")
            ceRow("serie") = RCCORRELATIVO.Rows(i).Item("SERAIL")
            ceRow("inicial") = RCCORRELATIVO.Rows(i).Item("CFKEYSP")
            ceRow("actual") = RCCORRELATIVO.Rows(i).Item("LSEG_LANUM")
            ceRow("final") = RCCORRELATIVO.Rows(i).Item("WPWDHENDE")
            If ceRow("actual") > ceRow("final") Then
                dtImportLog.Rows.Add(New String() {"Error", "Correlativos SAT", "", "importIntoCorrelativoSAP", "El correlativo actual es mayor al limite especificado por la resolucion.", "xoMobile"})
                Return False
            End If
            ceRow("usuarioCreacion") = RCCORRELATIVO.Rows(i).Item("ADMI_USER")
            ceRow("fechaCreacion") = RCCORRELATIVO.Rows(i).Item("AAADATUM")
            dsRCCORRELATIVO.Tables("RCCORRELATIVO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRCCORRELATIVO, "RCCORRELATIVO")
        oceClient.Conexion.Close()

        '--- Verificar correlativos activos
        If RCCORRELATIVO.Rows.Count < 3 Then
            dtImportLog.Rows.Add(New String() {"Error", "Correlativos SAT", "", "importIntoCorrelativoSAP", "No existen resuluciones activas para esta ruta", "xoMobile"})
            Return False
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Correlativos SAT", "", "importIntoCorrelativoSAP", RCCORRELATIVO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True
    End Function
#End Region
#Region " IMPORTAR CXC "
    Public Function importIntoRCXC(ByVal RCXC As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim delcmd As SqlCeCommand
        Dim dsRCXC As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RCXC", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRCXC, "RCXC")
        delcmd = New SqlCeCommand("DELETE FROM RCXC", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RCXC.Rows.Count - 1
            Dim ceRow As DataRow = dsRCXC.Tables("RCXC").NewRow()
            ceRow("id_cxc") = RCXC.Rows(i).Item("VBELN")
            ceRow("idCliente") = RCXC.Rows(i).Item("KUNNR")
            ceRow("dTipo") = RCXC.Rows(i).Item("BLART").ToString
            ceRow("serie") = RCXC.Rows(i).Item("BKTXT").ToString
            ceRow("numero") = RCXC.Rows(i).Item("XBLNR").ToString
            ceRow("fechaEmision") = RCXC.Rows(i).Item("BLDAT")
            ceRow("fechaVence") = RCXC.Rows(i).Item("VENCI")
            ceRow("importe") = RCXC.Rows(i).Item("WRBTR")
            ceRow("saldo") = RCXC.Rows(i).Item("ZSALDO")
            ceRow("porcentajeDesto") = 0
            ceRow("importeDesto") = RCXC.Rows(i).Item("KZWI6")
            If ceRow("saldo") > 0 Then
                dsRCXC.Tables("RCXC").Rows.Add(ceRow)
            End If
            ceRow("compromisoPago") = RCXC.Rows(i).Item("XREF1")
            ceRow("anioOpera") = RCXC.Rows(i).Item("GJAHR").ToString
            ceRow("doctoContable") = RCXC.Rows(i).Item("BELNR").ToString
            ceRow("apunteContable") = RCXC.Rows(i).Item("BUZEI").ToString
            ceRow("seriefel") = RCXC.Rows(i).Item("ZZSERFAC").ToString
            ceRow("numeroautorizacion") = RCXC.Rows(i).Item("ZZNUMFAC").ToString
            ceRow("tipoidrecep") = RCXC.Rows(i).Item("TIPOIDRECEP").ToString
            ceRow("idrecep") = RCXC.Rows(i).Item("IDRECEP").ToString
        Next
        ceDataAdapter.Update(dsRCXC, "RCXC")
        oceClient.Conexion.Close()
        If RCXC.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Cuenta por cobrar", "", "importIntoRCXC", RCXC.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Cuenta por cobrar", "", "importIntoRCXC", RCXC.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True
    End Function

    Public Function importIntoRCXC_DETALLE(ByVal RCXC_DETALLE As DataTable, ByRef dtImportLog As DataTable) As Boolean
        Dim delcmd As SqlCeCommand
        Dim dsRCXC_DETALLE As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RCXC_DETALLE", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRCXC_DETALLE, "RCXC_DETALLE")
        delcmd = New SqlCeCommand("DELETE FROM RCXC_DETALLE", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RCXC_DETALLE.Rows.Count - 1
            Dim ceRow As DataRow = dsRCXC_DETALLE.Tables("RCXC_DETALLE").NewRow()
            ceRow("idCxc") = RCXC_DETALLE.Rows(i).Item("VBELN")
            ceRow("linea") = RCXC_DETALLE.Rows(i).Item("POSNR")
            ceRow("idProducto") = RCXC_DETALLE.Rows(i).Item("MATNR")
            ceRow("unidades") = objUtil.isDecimal(RCXC_DETALLE.Rows(i).Item("FKIMG"))
            ceRow("cajas") = 0
            ceRow("importe") = 0
            ceRow("porcentajeDesto") = objUtil.isDecimal(RCXC_DETALLE.Rows(i).Item("WRBTR"))
            ceRow("importeDesto") = objUtil.isDecimal(RCXC_DETALLE.Rows(i).Item("KZWI6"))
            dsRCXC_DETALLE.Tables("RCXC_DETALLE").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRCXC_DETALLE, "RCXC_DETALLE")
        oceClient.Conexion.Close()
        If RCXC_DETALLE.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Detalle de cuentas por cobrar", "", "importIntoRCXC_DETALLE", RCXC_DETALLE.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Detalle de cuentas por cobrar", "", "importIntoRCXC_DETALLE", RCXC_DETALLE.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True
    End Function
#End Region
#Region " IMPORTAR DESPACHO "
    Public Function importIntoDESPACHO(ByVal RDESPACHO As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim delcmd As SqlCeCommand
        Dim dsRCXC As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from CFACTURA", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRCXC, "CFACTURA")
        delcmd = New SqlCeCommand("DELETE FROM CFACTURA", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RDESPACHO.Rows.Count - 1
            Dim ceRow As DataRow = dsRCXC.Tables("CFACTURA").NewRow()
            ceRow("id_pedido") = RDESPACHO.Rows(i).Item("VBELN").ToString
            ceRow("idCliente") = RDESPACHO.Rows(i).Item("KUNNR")
            ceRow("importe") = RDESPACHO.Rows(i).Item("NETWR")
            ceRow("moneda") = RDESPACHO.Rows(i).Item("WAERK").ToString

            '--- Descuento segun tipo de condicion de pago
            If RDESPACHO.Rows(i).Item("ZTERM").ToString = "IL01" Then
                ceRow("importeDesto") = RDESPACHO.Rows(i).Item("ZDESCUENTO").ToString
            Else
                ceRow("importeDestoPP") = RDESPACHO.Rows(i).Item("ZDESCUENTO").ToString
            End If

            ceRow("condicion") = RDESPACHO.Rows(i).Item("ZTERM").ToString
            ceRow("noEntrega") = RDESPACHO.Rows(i).Item("ZNUM_ENTREGA").ToString
            ceRow("noPedido") = RDESPACHO.Rows(i).Item("BSTNK").ToString
            ceRow("tTipo") = "PED"
            ceRow("idRuta") = id_glo_ruta
            ceRow("tPedido") = RDESPACHO.Rows(i).Item("AUART").ToString
            ceRow("tMotivo") = RDESPACHO.Rows(i).Item("AUGRU").ToString
            dsRCXC.Tables("CFACTURA").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRCXC, "CFACTURA")
        oceClient.Conexion.Close()

        '--- Si la ruta es despacho y no tiene despachos retorne error
        If RDESPACHO.Rows.Count = 0 And tipoRuta = "16" Then
            dtImportLog.Rows.Add(New String() {"Error", "Encabezado de despachos", "", "importIntoCFACTURA", RDESPACHO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return False
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Encabezado de despachos", "", "importIntoCFACTURA", RDESPACHO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True
    End Function

    Public Function importIntoDESPACHO_DETALLE(ByVal DESPACHO_DETALLE As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim delcmd As SqlCeCommand
        Dim dsDESPACHO_DETALLE As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from CFACTURA_DETALLE", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsDESPACHO_DETALLE, "CFACTURA_DETALLE")
        delcmd = New SqlCeCommand("DELETE FROM CFACTURA_DETALLE", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos

        For i As Integer = 0 To DESPACHO_DETALLE.Rows.Count - 1

            Dim ceRow As DataRow = dsDESPACHO_DETALLE.Tables("CFACTURA_DETALLE").NewRow()
            ceRow("id_pedido") = DESPACHO_DETALLE.Rows(i).Item("VBELN").ToString
            ceRow("item") = DESPACHO_DETALLE.Rows(i).Item("POSNR").ToString
            ceRow("idProducto") = DESPACHO_DETALLE.Rows(i).Item("MATNR").ToString
            ceRow("cantidad") = DESPACHO_DETALLE.Rows(i).Item("KWMENG").ToString
            ceRow("um") = DESPACHO_DETALLE.Rows(i).Item("VRKME").ToString
            ceRow("precio") = DESPACHO_DETALLE.Rows(i).Item("CMPRE_FLT").ToString
            ceRow("importe") = DESPACHO_DETALLE.Rows(i).Item("KZWI2").ToString
            ceRow("valorIvaImporte") = DESPACHO_DETALLE.Rows(i).Item("MWSBP").ToString
            ceRow("importeSinIva") = DESPACHO_DETALLE.Rows(i).Item("importeSinIva").ToString
            ceRow("litros") = DESPACHO_DETALLE.Rows(i).Item("VOLUM").ToString
            ceRow("importeDesto") = objUtil.isDecimal(DESPACHO_DETALLE.Rows(i).Item("KZWI5"))
            ceRow("valorIvaDesto") = 0
            ceRow("posicionSuperior") = DESPACHO_DETALLE.Rows(i).Item("UEPOS").ToString
            ceRow("porcentajeDesto") = 0
            ceRow("porcentajeDestoPP") = 0
            ceRow("isDiferente") = DESPACHO_DETALLE.Rows(i).Item("ZDIF_CANT")
            ceRow("candespacho") = DESPACHO_DETALLE.Rows(i).Item("LFIMG")
            ceRow("importeDestoPP") = DESPACHO_DETALLE.Rows(i).Item("KZWI6")
            ceRow("porcentajeDestoPP") = DESPACHO_DETALLE.Rows(i).Item("KZWI4")

            dsDESPACHO_DETALLE.Tables("CFACTURA_DETALLE").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsDESPACHO_DETALLE, "CFACTURA_DETALLE")
        oceClient.Conexion.Close()
        If DESPACHO_DETALLE.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Detalle de despachos", "", "importIntoDESPACHO_DETALLE", DESPACHO_DETALLE.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Detalle de despachos", "", "importIntoDESPACHO_DETALLE", DESPACHO_DETALLE.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True
    End Function
#End Region
#Region " IMPORTAR ENCUESTAS "

    Public Function importIntoRENCUESTA(ByVal RENCUESTA As DataTable, ByRef dtImportLog As DataTable) As Boolean
        Dim dsRencuesta As New DataSet
        Dim delcmd As SqlCeCommand

        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RENCUESTA", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRencuesta, "RENCUESTA")
        delcmd = New SqlCeCommand("DELETE FROM RENCUESTA", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()

        'Insertar datos
        For i As Integer = 0 To RENCUESTA.Rows.Count - 1
            Dim ceRow As DataRow = dsRencuesta.Tables("RENCUESTA").NewRow()
            ceRow("id_encuesta") = RENCUESTA.Rows(i).Item("ZCODIGO_ENCUESTA")
            ceRow("titulo") = RENCUESTA.Rows(i).Item("ZDESCRIPCION")
            ceRow("fechaInicio") = RENCUESTA.Rows(i).Item("ZFECHA_INI")
            ceRow("fechaFin") = RENCUESTA.Rows(i).Item("ZFECHA_FIN")
            dsRencuesta.Tables("RENCUESTA").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRencuesta, "RENCUESTA")
        If RENCUESTA.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Encuesta", "", "importIntoRENCUESTA", RENCUESTA.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            '--- Eliminar el workflow encuesta
            delcmd = New SqlCeCommand("DELETE FROM ttipo where tabla = 'workflow' AND DATAVALUE = '1'", oceClient.Conexion)
            ceDataAdapter.DeleteCommand = delcmd
            ceDataAdapter.DeleteCommand.ExecuteNonQuery()
            Return True
        End If
        oceClient.Conexion.Close()
        dtImportLog.Rows.Add(New String() {"Exito", "Encuesta", "", "importIntoRENCUESTA", RENCUESTA.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True
    End Function

    Public Function importIntorEncuesta_Temas(ByVal rEncuesta_Temas As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim delcmd As SqlCeCommand
        Dim dsrEncuesta_Temas As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from rEncuesta_Temas", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsrEncuesta_Temas, "rEncuesta_Temas")
        delcmd = New SqlCeCommand("DELETE FROM rEncuesta_Temas", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To rEncuesta_Temas.Rows.Count - 1
            Dim ceRow As DataRow = dsrEncuesta_Temas.Tables("rEncuesta_Temas").NewRow()
            ceRow("idEncuesta") = rEncuesta_Temas.Rows(i).Item("ZCODIGO_ENCUESTA")
            ceRow("id_tema") = rEncuesta_Temas.Rows(i).Item("ZCODIGO_TEMA")
            ceRow("orden") = rEncuesta_Temas.Rows(i).Item("ZORDEN")
            ceRow("tema") = rEncuesta_Temas.Rows(i).Item("ZTEMA")
            dsrEncuesta_Temas.Tables("rEncuesta_Temas").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsrEncuesta_Temas, "rEncuesta_Temas")
        oceClient.Conexion.Close()

        If rEncuesta_Temas.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Encuesta Temas", "", "importIntorEncuesta_Temas", rEncuesta_Temas.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Encuesta Temas", "", "importIntorEncuesta_Temas", rEncuesta_Temas.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function

    Public Function importIntorEncuesta_preguntas(ByVal rEncuesta_preguntas As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim delcmd As SqlCeCommand
        Dim dsrEncuesta_preguntas As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from rEncuesta_preguntas", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsrEncuesta_preguntas, "rEncuesta_preguntas")
        delcmd = New SqlCeCommand("DELETE FROM rEncuesta_preguntas", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To rEncuesta_preguntas.Rows.Count - 1
            Dim ceRow As DataRow = dsrEncuesta_preguntas.Tables("rEncuesta_preguntas").NewRow()
            ceRow("idEncuesta") = rEncuesta_preguntas.Rows(i).Item("ZCODIGO_ENCUESTA")
            ceRow("id_Pregunta") = rEncuesta_preguntas.Rows(i).Item("ZCODIGO_PREGUNTA")
            ceRow("idTema") = rEncuesta_preguntas.Rows(i).Item("ZCODIGO_TEMA")
            ceRow("orden") = rEncuesta_preguntas.Rows(i).Item("ZORDEN")
            ceRow("pregunta") = rEncuesta_preguntas.Rows(i).Item("ZPREGUNTA")
            ceRow("tipoRespuesta") = rEncuesta_preguntas.Rows(i).Item("ZTIPO_RESPUESTA")
            dsrEncuesta_preguntas.Tables("rEncuesta_Preguntas").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsrEncuesta_preguntas, "rEncuesta_preguntas")
        oceClient.Conexion.Close()
        If rEncuesta_preguntas.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Encuesta Preguntas", "", "importIntorEncuesta_preguntas", rEncuesta_preguntas.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Encuesta Preguntas", "", "importIntorEncuesta_preguntas", rEncuesta_preguntas.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function

    Public Function importIntoRENCUESTA_ALTERNATIVAS(ByVal RENCUESTA_ALTERNATIVAS As DataTable, ByRef dtImportLog As DataTable) As Boolean
        Try
            Dim delcmd As SqlCeCommand
            Dim dsRENCUESTA_ALTERNATIVAS As New DataSet
            oceClient.dbConnect()
            oceClient.Conexion.Open()
            Dim ceDataAdapter = New SqlCeDataAdapter("select * from RENCUESTA_ALTERNATIVAS", oceClient.Conexion)
            Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
            ceDataAdapter.Fill(dsRENCUESTA_ALTERNATIVAS, "RENCUESTA_ALTERNATIVAS")
            delcmd = New SqlCeCommand("DELETE FROM RENCUESTA_ALTERNATIVAS", oceClient.Conexion)
            ceDataAdapter.DeleteCommand = delcmd
            ceDataAdapter.DeleteCommand.ExecuteNonQuery()
            'Insertar datos
            For i As Integer = 0 To RENCUESTA_ALTERNATIVAS.Rows.Count - 1
                Dim ceRow As DataRow = dsRENCUESTA_ALTERNATIVAS.Tables("RENCUESTA_ALTERNATIVAS").NewRow()
                ceRow("idPregunta") = RENCUESTA_ALTERNATIVAS.Rows(i).Item("ZCODIGO_PREGUNTA")
                ceRow("alternativa") = RENCUESTA_ALTERNATIVAS.Rows(i).Item("ZALTERNATIVA")
                dsRENCUESTA_ALTERNATIVAS.Tables("RENCUESTA_ALTERNATIVAS").Rows.Add(ceRow)
            Next
            ceDataAdapter.Update(dsRENCUESTA_ALTERNATIVAS, "RENCUESTA_ALTERNATIVAS")
            oceClient.Conexion.Close()

            If RENCUESTA_ALTERNATIVAS.Rows.Count = 0 Then
                dtImportLog.Rows.Add(New String() {"Advertencia", "Encuesta Alternativas", "", "importIntoRENCUESTA_ALTERNATIVAS", RENCUESTA_ALTERNATIVAS.Rows.Count.ToString + " Registros importados ", "xoMobile"})
                Return True
            End If
            dtImportLog.Rows.Add(New String() {"Exito", "Encuesta Alternativas", "", "importIntoRENCUESTA_ALTERNATIVAS", RENCUESTA_ALTERNATIVAS.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Encuesta  alternativas", "", "importIntoRENCUESTA_ALTERNATIVAS", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region
#Region " IMPORTAR MATERIALES "
    Public Function importIntoRPRODUCTO(ByVal RPRODUCTO As DataTable, ByRef dtImportLog As DataTable) As Boolean



        Dim dsRPRODUCTO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RPRODUCTO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRPRODUCTO, "RPRODUCTO")
        'Insertar datos
        For i As Integer = 0 To RPRODUCTO.Rows.Count - 1
            Dim ceRow As DataRow = dsRPRODUCTO.Tables("RPRODUCTO").NewRow()
            ceRow("id_producto") = objUtil.isInteger(RPRODUCTO.Rows(i).Item("MATNR"))
            ceRow("idEnvase") = 0
            ceRow("idCaja") = 0
            ceRow("descripcion") = (RPRODUCTO.Rows(i).Item("MAKTX"))
            ceRow("categoria") = (RPRODUCTO.Rows(i).Item("KONDM"))
            ceRow("litrosUnidad") = objUtil.isDecimal2(RPRODUCTO.Rows(i).Item("VOLUM"))
            ceRow("unidadesCaja") = (RPRODUCTO.Rows(i).Item("UMREZ"))
            ceRow("tTipo") = objUtil.isInteger(RPRODUCTO.Rows(i).Item("MATKL"))
            ceRow("idMarca") = (RPRODUCTO.Rows(i).Item("MVGR2")) 'Campo nuevo de idMarca
            dsRPRODUCTO.Tables("RPRODUCTO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRPRODUCTO, "RPRODUCTO")
        oceClient.Conexion.Close()
        If RPRODUCTO.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Productos", "", "importIntoRPRODUCTO", RPRODUCTO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Productos", "", "importIntoRPRODUCTO", RPRODUCTO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR PREGUNTAS INICIALES Y FINALES "
    Public Function importIntoPreguntasInicialesFinales(ByVal RRUTA As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim delcmd As SqlCeCommand
        Dim dsRRUTA As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RSALIDA_INGRESO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRRUTA, "RSALIDA_INGRESO")
        delcmd = New SqlCeCommand("DELETE FROM RSALIDA_INGRESO", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RRUTA.Rows.Count - 1
            Dim ceRow As DataRow = dsRRUTA.Tables("RSALIDA_INGRESO").NewRow()
            ceRow("id_SalidaIngreso") = RRUTA.Rows(i).Item("IDPREG")
            ceRow("idRuta") = id_glo_ruta
            ceRow("pregunta") = RRUTA.Rows(i).Item("DESCRIP")
            ceRow("tipoDato") = RRUTA.Rows(i).Item("TIPODATO")
            ceRow("ttipo") = RRUTA.Rows(i).Item("FLGCLAVE")
            dsRRUTA.Tables("RSALIDA_INGRESO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRRUTA, "RSALIDA_INGRESO")
        oceClient.Conexion.Close()
        If RRUTA.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Preguntas Iniciales y Finales", "", "importIntoPreguntasInicialesFinales", RRUTA.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Preguntas Iniciales y Finales", "", "importIntoPreguntasInicialesFinales", RRUTA.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR PRESUPUESTO "
    Public Function importIntoRPRESUPUESTO(ByVal RPRESUPUESTO As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim dsRPRESUPUESTO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RPRESUPUESTO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRPRESUPUESTO, "RPRESUPUESTO")

        'Insertar datos
        For i As Integer = 0 To RPRESUPUESTO.Rows.Count - 1
            Dim ceRow As DataRow = dsRPRESUPUESTO.Tables("RPRESUPUESTO").NewRow()
            ceRow("idCliente") = objUtil.isInteger(RPRESUPUESTO.Rows(i).Item("KUNN2"))
            ceRow("litros") = objUtil.isInteger(RPRESUPUESTO.Rows(i).Item("FKIMG"))
            dsRPRESUPUESTO.Tables("RPRESUPUESTO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRPRESUPUESTO, "RPRESUPUESTO")
        oceClient.Conexion.Close()
        If RPRESUPUESTO.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Presupuesto", "", "importIntoRPRESUPUESTO", RPRESUPUESTO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Presupuesto", "", "importIntoRPRESUPUESTO", RPRESUPUESTO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True
    End Function
#End Region
#Region " IMPORTAR PRECIOS "
    Public Function importIntoZCAMPOS2(ByVal ZCAMPOS2 As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim delcmd As SqlCeCommand
        Dim dsZCAMPOS2 As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from ZCAMPOS2", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsZCAMPOS2, "ZCAMPOS2")
        delcmd = New SqlCeCommand("DELETE FROM ZCAMPOS2", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To ZCAMPOS2.Rows.Count - 1
            Dim ceRow As DataRow = dsZCAMPOS2.Tables("ZCAMPOS2").NewRow()
            ceRow("condicion") = ZCAMPOS2.Rows(i).Item("TEXT10")
            ceRow("secuencia") = ZCAMPOS2.Rows(i).Item("KOLNR")
            ceRow("tabla") = ZCAMPOS2.Rows(i).Item("KOTABNR")
            ceRow("orgVtas") = ZCAMPOS2.Rows(i).Item("VKORG")
            ceRow("canal") = ZCAMPOS2.Rows(i).Item("VTWEG")
            ceRow("claseCond") = ZCAMPOS2.Rows(i).Item("KSCHL")
            ceRow("id_cliente") = ZCAMPOS2.Rows(i).Item("KUNNR")
            ceRow("id_producto") = ZCAMPOS2.Rows(i).Item("MATNR")
            ceRow("fechaValidez") = ZCAMPOS2.Rows(i).Item("DATBI")
            ceRow("listaPrecios") = ZCAMPOS2.Rows(i).Item("PLTYP")
            ceRow("categoriaC") = ZCAMPOS2.Rows(i).Item("KONDA")
            ceRow("categoriaP") = ZCAMPOS2.Rows(i).Item("KONDM")
            ceRow("grupoVta") = ZCAMPOS2.Rows(i).Item("VKGRP")
            ceRow("ruta") = objUtil.isInteger(ZCAMPOS2.Rows(i).Item("SORTL"))
            ceRow("regla") = ZCAMPOS2.Rows(i).Item("KRECH")
            ceRow("importe") = ZCAMPOS2.Rows(i).Item("KBETR")
            ceRow("unidad") = ZCAMPOS2.Rows(i).Item("KONWA")
            ceRow("factor") = ZCAMPOS2.Rows(i).Item("KPEIN")
            ceRow("um") = ZCAMPOS2.Rows(i).Item("KMEIN")
            ceRow("isManual") = ZCAMPOS2.Rows(i).Item("KZNEP")
            dsZCAMPOS2.Tables("ZCAMPOS2").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsZCAMPOS2, "ZCAMPOS2")
        oceClient.Conexion.Close()
        If ZCAMPOS2.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Precios y descuentos", "", "importIntoZCAMPOS2", ZCAMPOS2.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Precios y descuentos", "", "importIntoZCAMPOS2", ZCAMPOS2.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR PRECIO ACCESOS "
    Public Function importIntoRPRECIO_ACCESO(ByVal RPRECIO_ACCESO As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim delcmd As SqlCeCommand
        Dim dsRPRECIO_ACCESO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RPRECIO_ACCESO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRPRECIO_ACCESO, "RPRECIO_ACCESO")
        delcmd = New SqlCeCommand("DELETE FROM RPRECIO_ACCESO", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        'ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RPRECIO_ACCESO.Rows.Count - 1
            Dim ceRow As DataRow = dsRPRECIO_ACCESO.Tables("RPRECIO_ACCESO").NewRow()
            ceRow("acc") = RPRECIO_ACCESO.Rows(i).Item("KOLNR")
            ceRow("tabla") = RPRECIO_ACCESO.Rows(i).Item("KOTABNR")
            dsRPRECIO_ACCESO.Tables("RPRECIO_ACCESO").Rows.Add(ceRow)
        Next
        ' ceDataAdapter.Update(dsRPRECIO_ACCESO, "RPRECIO_ACCESO")
        oceClient.Conexion.Close()

        If RPRECIO_ACCESO.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Secuencia de acceso", "", "importIntoRPRECIO_ACCESO", RPRECIO_ACCESO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Secuencia de acceso", "", "importIntoRPRECIO_ACCESO", RPRECIO_ACCESO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR RANGOS DE FECHA PARA DIAS CREDITO"
    Public Function importIntoRANGO(ByVal RANGO As DataTable, ByRef dtImportLog As DataTable) As Boolean

        Dim friendlyFunction As String
        Dim dsRASIGNA_VIAS_PAGO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RRANGO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRASIGNA_VIAS_PAGO, "RRANGO")

        'Insertar datos
        For i As Integer = 0 To RANGO.Rows.Count - 1
            Dim ceRow As DataRow = dsRASIGNA_VIAS_PAGO.Tables("RRANGO").NewRow()
            ceRow("categoriac") = RANGO.Rows(i).Item("KONDA")
            ceRow("fechainicio") = RANGO.Rows(i).Item("ARCH_DATS")
            ceRow("fechafin") = RANGO.Rows(i).Item("AAADATUM")
            ceRow("idcondicion") = RANGO.Rows(i).Item("ZTERM")
            dsRASIGNA_VIAS_PAGO.Tables("RRANGO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRASIGNA_VIAS_PAGO, "RRANGO")
        oceClient.Conexion.Close()
        friendlyFunction = "Rangos de condiciones de pago."

        If RANGO.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", friendlyFunction, "", "importIntoRANGO", RANGO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return False
        End If
        dtImportLog.Rows.Add(New String() {"Exito", friendlyFunction, "", "importIntoRANGO", RANGO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR RAZONES DE NO VISITA "
    Public Function importIntoRazonesNV(ByVal TTIPO As DataTable, ByRef dtImportLog As DataTable) As Boolean
        Dim delcmd As SqlCeCommand

        Dim dsTTIPO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from TTIPO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsTTIPO, "TTIPO")
        delcmd = New SqlCeCommand("DELETE FROM TTIPO where tabla like '%RAZONES_NO_ATENCION%'", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()

        'Insertar datos
        For i As Integer = 0 To TTIPO.Rows.Count - 1
            Dim ceRow As DataRow = dsTTIPO.Tables("TTIPO").NewRow()
            ceRow("tabla") = "RAZONES_NO_ATENCION"
            ceRow("showValue") = TTIPO.Rows(i).Item("BEZEI")
            ceRow("dataValue") = TTIPO.Rows(i).Item("ABRVW")
            dsTTIPO.Tables("TTIPO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsTTIPO, "TTIPO")
        oceClient.Conexion.Close()
        If TTIPO.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Error", "Razones no visita", "", "importIntoWorkflowSAP", TTIPO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return False
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Razones no visita", "", "importIntoWorkflowSAP", TTIPO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True
    End Function
#End Region
#Region " IMPORTAR RUTA "
    Public Function importIntoRRUTA(ByVal RRUTA As DataTable, ByRef dtImportLog As DataTable) As Boolean
        Dim delcmd As SqlCeCommand
        Dim dsRRUTA As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RRUTA", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRRUTA, "RRUTA")
        delcmd = New SqlCeCommand("DELETE FROM RRUTA", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RRUTA.Rows.Count - 1
            Dim ceRow As DataRow = dsRRUTA.Tables("RRUTA").NewRow()
            ceRow("codRuta") = RRUTA.Rows(i).Item("KUNNR")
            ceRow("tTipo") = RRUTA.Rows(i).Item("TEXT5")
            ceRow("nomEmpresa") = RRUTA.Rows(i).Item("NAME1")
            ceRow("direccion") = RRUTA.Rows(i).Item("STRAS")
            ceRow("telefono") = RRUTA.Rows(i).Item("TELF1")
            ceRow("nit") = RRUTA.Rows(i).Item("STCD1")
            ceRow("clienteGenerico") = RRUTA.Rows(i).Item("KUNN2")
            ceRow("maxVentaCG") = RRUTA.Rows(i).Item("WEBTR")
            ceRow("fechaEmision") = Date.Now.ToString 'RRUTA.Rows(i).Item("ERDAT")
            ceRow("esActual") = "true"
            ceRow("confirmado") = "false"
            ceRow("exportar") = "false"
            ceRow("importar") = "false"
            ceRow("tipoRuta") = RRUTA.Rows(i).Item("LPRIO")
            tipoRuta = RRUTA.Rows(i).Item("LPRIO")
            ceRow("sociedad") = RRUTA.Rows(i).Item("BUKRS")
            'Se modifico para validar que rutas tienen acceso a FEL este campo viene con el valor de "X"
            ceRow("fel") = RRUTA.Rows(i).Item("FEL")
            ceRow("cui_hh") = RRUTA.Rows(i).Item("CUI_HH")
            dsRRUTA.Tables("RRUTA").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRRUTA, "RRUTA")
        oceClient.Conexion.Close()
        If RRUTA.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Error", "Parametros de Ruta", "", "importIntoRRUTA", RRUTA.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return False
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Parametros de Ruta", "", "importIntoRRUTA", RRUTA.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR RCONTINGENCIA "
    Public Function importIntoRContingencia(ByVal RCONTINGENCIA As DataTable, ByRef dtImportLog As DataTable) As Boolean
        Dim dsRContingencia As New DataSet
        Dim delcmd As SqlCeCommand
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RCONTINGENCIA", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRContingencia, "RCONTINGENCIA")
        delcmd = New SqlCeCommand("DELETE FROM RCONTINGENCIA", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RCONTINGENCIA.Rows.Count - 1
            Dim ceRow As DataRow = dsRContingencia.Tables("RCONTINGENCIA").NewRow()
            ceRow("sociedad") = id_glo_sociedad
            ceRow("ruta") = RCONTINGENCIA.Rows(i).Item("RUTA")
            ceRow("numeroAcceso") = RCONTINGENCIA.Rows(i).Item("NUM_ACCESO")
            ceRow("referencia") = RCONTINGENCIA.Rows(i).Item("REFERENCIA")
            ceRow("usado") = RCONTINGENCIA.Rows(i).Item("USADO")
            ceRow("registrado") = RCONTINGENCIA.Rows(i).Item("REGISTRADO")
            'ceRow("fecha") = RCONTINGENCIA.Rows(i).Item("REGDAT")
            'ceRow("hora") = RCONTINGENCIA.Rows(i).Item("REGZEIT")
            dsRContingencia.Tables("RCONTINGENCIA").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRContingencia, "RCONTINGENCIA")
        oceClient.Conexion.Close()
        If RCONTINGENCIA.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Productos", "", "importIntoRContingencia", RCONTINGENCIA.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Productos", "", "importIntoRContingencia", RCONTINGENCIA.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True
    End Function
#End Region

#Region " IMPORTAR TIPOS "
    Public Function importIntoTTIPO(ByVal TTIPO As DataTable, ByRef dtImportLog As DataTable) As Boolean

        '--- Sentencia por SQL command
        Dim dsTTIPO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from TTIPO ", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsTTIPO, "TTIPO")

        '--- Insertar datos
        For i As Integer = 0 To TTIPO.Rows.Count - 1
            Dim ceRow As DataRow = dsTTIPO.Tables("TTIPO").NewRow()
            ceRow("grupo") = TTIPO.Rows(i).Item("GRUPO")
            ceRow("tabla") = TTIPO.Rows(i).Item("PARAMETRO")
            ceRow("dataValue") = TTIPO.Rows(i).Item("CORRELATIVO")
            ceRow("showValue") = TTIPO.Rows(i).Item("VALOR")
            dsTTIPO.Tables("TTIPO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsTTIPO, "TTIPO")
        oceClient.Conexion.Close()
        If TTIPO.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Tipos", "", "importIntoTTIPO", TTIPO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Tipos", "", "importIntoTTIPO", TTIPO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR USUARIOS "
    Public Function importIntoRUSUARIO(ByVal RUSUARIO As DataTable, ByRef dtImportLog As DataTable) As Boolean

        '--- Sentencia por SQL command
        Dim dsRUSUARIO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RUSUARIO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRUSUARIO, "RUSUARIO")
        'Insertar datos
        For i As Integer = 0 To RUSUARIO.Rows.Count - 1
            Dim ceRow As DataRow = dsRUSUARIO.Tables("RUSUARIO").NewRow()
            ceRow("id_usuario") = RUSUARIO.Rows(i).Item("PERSNO")
            ceRow("rol") = RUSUARIO.Rows(i).Item("ZPUESTO")
            ceRow("nombre") = RUSUARIO.Rows(i).Item("FULL_NAME")
            ceRow("password") = RUSUARIO.Rows(i).Item("ZPASWORD")
            dsRUSUARIO.Tables("RUSUARIO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRUSUARIO, "RUSUARIO")
        oceClient.Conexion.Close()

        If RUSUARIO.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Usuarios", "", "importIntoRUSUARIO", RUSUARIO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Usuarios", "", "importIntoRUSUARIO", RUSUARIO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region
#Region " IMPORTAR VIAS DE PAGO "
    Public Function importIntoRVIAS_PAGO(ByVal RVIAS_PAGO As DataTable, ByRef dtImportLog As DataTable) As Boolean
        Dim delcmd As SqlCeCommand
        Dim dsRVIAS_PAGO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RVIAS_PAGO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsRVIAS_PAGO, "RVIAS_PAGO")
        delcmd = New SqlCeCommand("DELETE FROM RVIAS_PAGO", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To RVIAS_PAGO.Rows.Count - 1
            Dim ceRow As DataRow = dsRVIAS_PAGO.Tables("RVIAS_PAGO").NewRow()
            ceRow("via") = RVIAS_PAGO.Rows(i).Item("ZLSCH")
            ceRow("descripcion") = RVIAS_PAGO.Rows(i).Item("TEXT1")
            ceRow("simbolo") = RVIAS_PAGO.Rows(i).Item("SIGNO")
            dsRVIAS_PAGO.Tables("RVIAS_PAGO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRVIAS_PAGO, "RVIAS_PAGO")
        oceClient.Conexion.Close()
        If RVIAS_PAGO.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "Catalogo de vias de pago", "", "importIntoRVIAS_PAGO", RVIAS_PAGO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return False
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Catalogo de vias de pago", "", "importIntoRVIAS_PAGO", RVIAS_PAGO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True

    End Function
#End Region




#Region " IMPORTAR WORKFLOW SAP "
    Public Function importIntoWorkflowSAP(ByVal TTIPO As DataTable, ByRef dtImportLog As DataTable) As Boolean
        Dim delcmd As SqlCeCommand
        Dim dsTTIPO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from TTIPO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)

        ceDataAdapter.Fill(dsTTIPO, "TTIPO")
        delcmd = New SqlCeCommand("DELETE FROM TTIPO where tabla like '%work%'", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()

        '---Insertar registros
        For i As Integer = 0 To TTIPO.Rows.Count - 1
            Dim ceRow As DataRow = dsTTIPO.Tables("TTIPO").NewRow()
            ceRow("tabla") = TTIPO.Rows(i).Item("TEXT30")
            ceRow("showValue") = TTIPO.Rows(i).Item("TEXT25")
            ceRow("dataValue") = TTIPO.Rows(i).Item("DMS_ORDER_NR")
            dsTTIPO.Tables("TTIPO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsTTIPO, "TTIPO")
        oceClient.Conexion.Close()

        If TTIPO.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Error", "Workflow de atencion", "", "importIntoWorkflowSAP", TTIPO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return False
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Workflow de atencion", "", "importIntoWorkflowSAP", TTIPO.Rows.Count.ToString + " Registros importados ", "xoMobile"})

        '--- Indicar si se debe validar inventario en la carga de producto al camion
        Dim workFlow As New workflowBL
        If workFlow.existeOperacion("Venta") Or workFlow.existeOperacion("Despacho") Then
            xo_validaInventario = True
        Else
            xo_validaInventario = False
        End If
        Return True
    End Function
#End Region


#Region " IMPORTAR WORKFLOW SAP GIRO"
    Public Function importIntoWorkflowSAPGiro(ByVal RWORKFLOW_GIRO As DataTable, ByRef dtImportLog As DataTable) As Boolean
        Dim delcmd As SqlCeCommand
        Dim dsRWORKFLOW_GIRO As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RWORKFLOW_GIRO", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)

        ceDataAdapter.Fill(dsRWORKFLOW_GIRO, "RWORKFLOW_GIRO")
        delcmd = New SqlCeCommand("DELETE FROM RWORKFLOW_GIRO", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()

        '---Insertar registros
        For i As Integer = 0 To RWORKFLOW_GIRO.Rows.Count - 1
            Dim ceRow As DataRow = dsRWORKFLOW_GIRO.Tables("RWORKFLOW_GIRO").NewRow()
            ceRow("sociedad") = RWORKFLOW_GIRO.Rows(i).Item("BUKRS")
            ceRow("giro") = RWORKFLOW_GIRO.Rows(i).Item("BRSCH")
            ceRow("nvalue") = RWORKFLOW_GIRO.Rows(i).Item("TEXT25")
            ceRow("toperacion") = RWORKFLOW_GIRO.Rows(i).Item("DMS_ORDER_NR")
            ceRow("tabla") = RWORKFLOW_GIRO.Rows(i).Item("TEXT30")
            ceRow("requerido") = RWORKFLOW_GIRO.Rows(i).Item("ZREQUERIDO")
            dsRWORKFLOW_GIRO.Tables("RWORKFLOW_GIRO").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRWORKFLOW_GIRO, "RWORKFLOW_GIRO")
        oceClient.Conexion.Close()

        If RWORKFLOW_GIRO.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Error", "Workflow de Giro", "", "importIntoWorkflowSAPGiro", RWORKFLOW_GIRO.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return False
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Workflow de Giro", "", "importIntoWorkflowSAPGiro", RWORKFLOW_GIRO.Rows.Count.ToString + " Registros importados ", "xoMobile"})

        '--- Indicar si se debe validar inventario en la carga de producto al camion
        'Dim workFlow As New workflowBL
        'If workFlow.existeOperacion("Venta") Or workFlow.existeOperacion("Despacho") Then
        'xo_validaInventario = True
        'Else
        'xo_validaInventario = False
        'End If
        Return True
    End Function
#End Region


#Region " IMPORTAR PRESUPUESTO CLIENTE POR MARCA"
    Public Function importIntoPresupuestoCliente(ByVal RPRESUPUESTO_CLIENTE As DataTable, ByRef dtImportLog As DataTable) As Boolean
        Dim delcmd As SqlCeCommand
        Dim dsRPRESUPUESTO_CLIENTE As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RPRESUPUESTO_CLIENTE", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)

        ceDataAdapter.Fill(dsRPRESUPUESTO_CLIENTE, "RPRESUPUESTO_CLIENTE")
        delcmd = New SqlCeCommand("DELETE FROM RPRESUPUESTO_CLIENTE", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()

        '---Insertar registros
        For i As Integer = 0 To RPRESUPUESTO_CLIENTE.Rows.Count - 1
            Dim ceRow As DataRow = dsRPRESUPUESTO_CLIENTE.Tables("RPRESUPUESTO_CLIENTE").NewRow()
            ceRow("idcliente") = RPRESUPUESTO_CLIENTE.Rows(i).Item("KUNNR")
            ceRow("marca") = RPRESUPUESTO_CLIENTE.Rows(i).Item("MARCA")
            ceRow("lit_presupuesto") = RPRESUPUESTO_CLIENTE.Rows(i).Item("LIT_PRESUPUESTO")
            ceRow("mt_presupuesto") = RPRESUPUESTO_CLIENTE.Rows(i).Item("MT_PRESUPUESTO")
            ceRow("lit_real") = RPRESUPUESTO_CLIENTE.Rows(i).Item("LIT_REAL")
            ceRow("venta_real") = RPRESUPUESTO_CLIENTE.Rows(i).Item("VENTA_REAL")
            ceRow("nec_lit_dia") = RPRESUPUESTO_CLIENTE.Rows(i).Item(6)
            ceRow("nec_vta_dia") = RPRESUPUESTO_CLIENTE.Rows(i).Item(7)
            ceRow("nec_lit_mes") = RPRESUPUESTO_CLIENTE.Rows(i).Item(8)
            ceRow("nec_vta_mes") = RPRESUPUESTO_CLIENTE.Rows(i).Item(9)
            dsRPRESUPUESTO_CLIENTE.Tables("RPRESUPUESTO_CLIENTE").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsRPRESUPUESTO_CLIENTE, "RPRESUPUESTO_CLIENTE")
        oceClient.Conexion.Close()

        If RPRESUPUESTO_CLIENTE.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Error", "Presupuesto Cliente", "", "importIntoPresupuestoCliente", RPRESUPUESTO_CLIENTE.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return False
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "Presupuesto Cliente", "", "importIntoPresupuestoCliente", RPRESUPUESTO_CLIENTE.Rows.Count.ToString + " Registros importados ", "xoMobile"})

        '--- Indicar si se debe validar inventario en la carga de producto al camion
        'Dim workFlow As New workflowBL
        'If workFlow.existeOperacion("Venta") Or workFlow.existeOperacion("Despacho") Then
        'xo_validaInventario = True
        'Else
        'xo_validaInventario = False
        'End If
        Return True
    End Function
#End Region

#Region " IMPORTAR DEPTOS Y MUNICIPIOS"
    Public Function importIntoDeptos(ByVal DEPTOS As DataTable, ByRef dtImportLog As DataTable) As Boolean
        '--- Sentencia por SQL command
        Dim delcmd As SqlCeCommand
        Dim dsDEPTOS As New DataSet
        oceClient.dbConnect()
        oceClient.Conexion.Open()
        Dim ceDataAdapter = New SqlCeDataAdapter("select * from RDEPTO_MUNI", oceClient.Conexion)
        Dim ceCmdBuilder = New SqlCeCommandBuilder(ceDataAdapter)
        ceDataAdapter.Fill(dsDEPTOS, "RDEPTO_MUNI")
        delcmd = New SqlCeCommand("DELETE FROM RDEPTO_MUNI", oceClient.Conexion)
        ceDataAdapter.DeleteCommand = delcmd
        ceDataAdapter.DeleteCommand.ExecuteNonQuery()
        'Insertar datos
        For i As Integer = 0 To DEPTOS.Rows.Count - 1
            Dim ceRow As DataRow = dsDEPTOS.Tables("RDEPTO_MUNI").NewRow()
            ceRow("DEPTO") = DEPTOS.Rows(i).Item("DEPTO")
            ceRow("NOM_DEPTO") = DEPTOS.Rows(i).Item("NOM_DEPTO")
            ceRow("MUNI") = DEPTOS.Rows(i).Item("MUNI")
            ceRow("NOM_MUNI") = DEPTOS.Rows(i).Item("NOM_MUNI")
            dsDEPTOS.Tables("RDEPTO_MUNI").Rows.Add(ceRow)
        Next
        ceDataAdapter.Update(dsDEPTOS, "RDEPTO_MUNI")
        oceClient.Conexion.Close()

        If DEPTOS.Rows.Count = 0 Then
            dtImportLog.Rows.Add(New String() {"Advertencia", "RDEPTO_MUNI", "", "importIntoDeptos", DEPTOS.Rows.Count.ToString + " Registros importados ", "xoMobile"})
            Return True
        End If
        dtImportLog.Rows.Add(New String() {"Exito", "RDEPTO_MUNI", "", "importIntoDeptos", DEPTOS.Rows.Count.ToString + " Registros importados ", "xoMobile"})
        Return True
    End Function
#End Region



#Region " PROCESO COMPLEMETARIO"
    Public Function importacionComplemento(ByRef dtImportLog As DataTable) As Boolean
        Dim oProducto As New ProductoDT
        Dim oCliente As New ClienteDT
        Dim oUtil As New UtilitarioDT

        '--- Boom
        'Dim dtProducto As New DataTable
        Dim objProducto As New ProductoBL
        'dtProducto = oProducto.getProductos()
        oProducto.bulkBoom(2)
        oProducto.bulkBoom(3)

        '--- Actualizar el credito disponible del cliente
        oCliente.setCreditoDisponible()

        '--- Ttipo
        oUtil.invertirTipo()

        '--- Convertir las cajas de la carga basica en unidades
        oProducto.bulkCajasUnidades()

        '--- Crear copia de la base de datos
        Dim ce As New ceClient
        ce.sdf_copy()
        Return True
    End Function

    Public Function completarDespachos() As rLayerHandler
        Dim despacho As New DespachoDT
        Dim rLayer As New rLayerHandler
        despacho.setCompletarDespacho(rLayer)

        '--- Evaluar error y enviar mensaje
        rLayer.evaluarError()
        Return rLayer
    End Function
#End Region
End Class

