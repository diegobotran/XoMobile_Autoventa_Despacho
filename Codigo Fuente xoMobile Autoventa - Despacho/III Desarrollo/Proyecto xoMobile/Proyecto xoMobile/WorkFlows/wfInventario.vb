Imports System.Data
Imports System.Threading
Imports Proyecto_xoMobile_Packs


Public Class workFlowInventario

#Region " DECLARACION DE VARIABLESY OBJETOS "
    Public comCliente As ClienteCO
    Private docInventario As New documentoCO
    Private rLayer As New rLayerHandler
    Private paso As Integer
    Private corriendo As Boolean = False
    Private lstInventario As New Windows.Forms.ListView
#End Region

    Public Function ejecutarInventario() As Boolean
        corriendo = True
        paso = 1

        '--- Pasos para ejecutar una preventa
        While corriendo
            Select Case paso
                Case Is = 1
                    stepCargaInventario()
                Case Is = 2
                    stepTomaInventario()
                Case Is = 3
                    confirmar()
            End Select
        End While

        If rLayer.codigo = 3 Then
            Return False
        Else
            Return True
        End If
    End Function


#Region " PASOS INVENTARIO "

    Public Function stepTomaInventario() As Boolean

        '--- Realizar el ingreso 
        Dim frmIngreso As New frmIngreso
        frmIngreso.v_com_cliente = comCliente
        frmIngreso.lblNombreCliente.Text = comCliente.negocio
        frmIngreso.lblAtencion.Text = "Inventario"
        frmIngreso.lstAgregadosExternal = lstInventario
        id_glo_aplicacion = "inventario"
        frmIngreso.ShowDialog()

        '--- Evaluar la ejecucion del proceso
        rLayer = frmIngreso.rlayer
        Select Case rLayer.codigo
            Case 3

                '--- Codigo de aborto de la operacion
                corriendo = False
                Return False
        End Select

        '--- Recuperar objetos del proceso 
        docInventario = frmIngreso.comDocumento
        lstInventario = frmIngreso.lstAgregados
        docInventario.ttipo = "INV"
        paso = 3
        Return True
    End Function

    Private Function stepCargaInventario() As Boolean
        Dim objInventario As New InventarioCliBL
        Dim dtInventario As New DataTable
        Dim objUtilBL As New UtilitarioBL

        '--- Recuperar el inventario de este cliente
        dtInventario = objInventario.listarInventarioFisico(comCliente.codigo, rLayer)
        If Not dtInventario Is Nothing Then
            '--- Llena datos del listado
            objInventario.llenaListado(dtInventario, lstInventario)
        End If
        paso = 2
    End Function

#End Region

    Private Function confirmar() As Boolean
        Cursor.Current = Cursors.WaitCursor
        confirmarInventario()
        Cursor.Current = Cursors.Default

        'Dim t As New ThreadStart(AddressOf Me.confirmarInventario)
        'Dim mt As New Thread(t)
        'mt.Start()
    End Function

    Private Function confirmarInventario() As Boolean

        Dim objInventario As New InventarioCliBL
        Dim ce As New ceClient

        '--- Crear inventario en la base de datos
        objInventario.agregarInventarioFisico(lstInventario)

        '--- Indicar que la transaccion finaliza
        corriendo = False

        '--- Ejecutar Backup de la base de datos
        ce.sdf_backup()
        Return True
    End Function



End Class
