Imports System.Data
Imports Proyecto_xoMobile_Packs

Public Class workflowBL
    Dim oWorkFlow As New WorkflowDT


    Public Function crearWorkflow(ByVal cliente As ClienteCO, ByRef rlayer As rLayerHandler) As DataTable

        Dim dtWorkflow As New DataTable
        dtWorkflow = oWorkFlow.getWorkflow(cliente.codigo, rlayer)

        ''--- Determinar si es cliente generico o nuevo
        If cliente.codigo = id_glo_clienteGenerico Then
            Dim dvWorkFlow As New DataView(dtWorkflow)
            dvWorkFlow.RowFilter = "showValue not in ('Encuesta','Cobro','Inventario')"
            dtWorkflow = dvWorkFlow.ToTable
        End If

        '--- Validar
        rlayer.evaluaTabla(dtWorkflow)

        '--- Evaluar error y enviar mensaje
        rlayer.evaluarError()

        Return dtWorkflow
    End Function

    Public Function crearWorkflowCV(ByVal cliente As ClienteCO, ByRef rlayer As rLayerHandler) As DataTable

        Dim dtWorkflow As New DataTable
        dtWorkflow = oWorkFlow.getWorkflow(cliente.codigo, rlayer)

        ''--- Determinar si es cliente generico o nuevo
        If cliente.codigo = id_glo_clienteGenerico Or cliente.tipoDi = "NUEVO" Or cliente.tipoDi = "TEMP" Then
            Dim dvWorkFlow As New DataView(dtWorkflow)
            dvWorkFlow.RowFilter = "showValue not in ('Encuesta','Cobro','Inventario')"
            dtWorkflow = dvWorkFlow.ToTable
        End If

        '--- Validar
        rlayer.evaluaTabla(dtWorkflow)

        '--- Evaluar error y enviar mensaje
        rlayer.evaluarError()
        Return dtWorkflow
    End Function

    Public Function crearWorkflow2(ByVal cliente As ClienteCO, ByRef rlayer As rLayerHandler) As DataTable

        Dim dtWorkflow As New DataTable
        dtWorkflow = oWorkFlow.getWorkflow2(cliente.codigo, rlayer)

        '--- Determinar si es cliente generico o nuevo
        'If cliente.codigo = id_glo_clienteGenerico Or cliente.tipoDi = "NUEVO" Or cliente.tipoDi = "TEMP" Then
        ' Dim dvWorkFlow As New DataView(dtWorkflow)
        ' dvWorkFlow.RowFilter = "nvalue not in ('Encuesta','Cobro','Inventario')"
        'dtWorkflow = dvWorkFlow.ToTable
        'End If

        '--- Validar
        rlayer.evaluaTabla(dtWorkflow)

        '--- Evaluar error y enviar mensaje
        rlayer.evaluarError()
        Return dtWorkflow
    End Function

    Public Function crearWorkflow3(ByVal cliente As ClienteCO, ByRef rlayer As rLayerHandler) As DataTable

        Dim dtWorkflow As New DataTable
        dtWorkflow = oWorkFlow.getWorkflow3(cliente.codigo, rlayer)

        '--- Determinar si es cliente generico o nuevo
        'If cliente.codigo = id_glo_clienteGenerico Or cliente.tipoDi = "NUEVO" Or cliente.tipoDi = "TEMP" Then
        'Dim dvWorkFlow As New DataView(dtWorkflow)
        'dvWorkFlow.RowFilter = "nvalue not in ('Encuesta','Cobro','Inventario')"
        'dtWorkflow = dvWorkFlow.ToTable
        'End If

        '--- Validar
        rlayer.evaluaTabla(dtWorkflow)

        '--- Evaluar error y enviar mensaje
        rlayer.evaluarError()
        Return dtWorkflow
    End Function

    Public Function statusWorkflow2(ByRef Workflow As WorkFlowCo, ByVal cliente As ClienteCO, ByRef rlayer As rLayerHandler, ByVal menu As Windows.Forms.ListView) As Boolean
        Workflow.NingunaRealizada = True
        Workflow.TodasRealizadas = True

        For i = 0 To menu.Items.Count - 3
            Dim operacion As String
            Dim realizado As Boolean
            operacion = Trim(menu.Items(i).Text.ToLower)
            If (menu.Items(i).ForeColor = Color.DarkGreen Or menu.Items(i).ForeColor = Color.Gray) Then
                realizado = True
                Workflow.NingunaRealizada = False
            Else
                realizado = False
                Workflow.TodasRealizadas = False
            End If

            Select Case operacion
                Case "encuesta"
                    Workflow.encuesta = realizado
                Case "cobro"
                    Workflow.cobro = realizado
                Case "inventario"
                    Workflow.inventario = realizado
                Case "venta"
                    Workflow.venta = realizado
                Case "despacho"
                    Workflow.despacho = realizado
            End Select
        Next
        Return True
    End Function

    Public Function existeOperacion(ByVal operacion As String) As Boolean
        Dim oUtil As New UtilitarioBL
        If Not oUtil.getDataValue("WORKFLOW", operacion) = "0" Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
