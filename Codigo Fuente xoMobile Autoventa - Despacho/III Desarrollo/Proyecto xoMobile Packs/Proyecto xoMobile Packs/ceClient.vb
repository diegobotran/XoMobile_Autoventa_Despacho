Imports System.Data.SqlServerCe
Imports System.Data
Imports System.IO

Public Class ceClient
    Public Conexion As SqlCeConnection

    Public Sub sdf_copy()
        Dim WorkingDirectory As String = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))

        ' Specify the directories you want to manipulate.
        Dim path As String = WorkingDirectory + "\xomData.sdf"
        Dim path2 As String = WorkingDirectory + "\respaldo_sdf\xomData.sdf"

        Try
            Directory.CreateDirectory(WorkingDirectory + "\respaldo_sdf\")

            ' Ensure that the target does not exist.
            File.Delete(path2)

            ' Copy the file.
            File.Copy(path, path2, True)
        Catch ex As Exception
            MsgBox("Error al crear la base de datos de respaldo: " + ex.Message)
        End Try
    End Sub

    Public Sub sdf_restore()
        Dim WorkingDirectory As String = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))

        ' Specify the directories you want to manipulate.
        Dim path As String = (WorkingDirectory + "\xomData.sdf")
        Dim path2 As String = (WorkingDirectory + "\respaldo_sdf\xomData.sdf")
        Try
            ' Ensure that the target does not exist.
            File.Delete(path)

            ' Copy the file.
            File.Copy(path2, path, True)
        Catch ex As Exception
            MsgBox("Error al restaurar la base de datos de respaldo: " + ex.Message)
        End Try
    End Sub

    Public Sub sdf_restoreFromBackup()
        Dim WorkingDirectory As String = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))
        ' Specify the directories you want to manipulate.
        Dim path As String = (WorkingDirectory + "\xomData.sdf")
        Dim path2 As String = (WorkingDirectory + "\respaldoTransaccional_sdf\xomData.sdf")
        File.Copy(path2, path, True)
        'bitacora
        'objBitacora.registrarOperacion(41, id_glo_cliente)
    End Sub

    Public Sub sdf_backup()
        Dim WorkingDirectory As String = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))

        ' Specify the directories you want to manipulate.
        Dim path As String = (WorkingDirectory + "\xomData.sdf")
        Dim path2 As String = (WorkingDirectory + "\respaldoTransaccional_sdf\xomData.sdf")

        ' Ensure that the target does not exist.
        File.Delete(path2)

        ' Copy the file.
        File.Copy(path, path2, True)



    End Sub

#Region "Cadena de conexion"
    Public Sub shrink()
        Try
            Dim cadenaDeConexion = "Data Source=" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\xomData.sdf;"
            Dim engine As New SqlCeEngine(cadenaDeConexion)
            engine.Shrink()
            engine.Compact(cadenaDeConexion)
            engine.Dispose()
        Catch ex As Exception
        End Try
    End Sub


    ''' <summary>
    ''' Crea la cadena de conexion a la base de datos SQL-CE, esta cadena de conexion es utilizada por los Seter y Geter
    ''' </summary>
    ''' <returns>SqlCeConnection</returns>
    ''' <remarks></remarks>
    Public Function dbConnect() As SqlCeConnection
        Dim cadenaDeConexion As String
        cadenaDeConexion = "Data Source=" + (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)) + "\\xomData.sdf;"
        'cadenaDeConexion = "Data Source=C:\xomobile\xomData.sdf;"

        Conexion = New SqlCeConnection(cadenaDeConexion)
        Return Conexion
    End Function
#End Region

#Region "Getter y Setter"
    ''' <summary>
    ''' Ejecuta una sentencia SQL en la base de dtos SQL-CE del tipo SELECT para obtener un bloque de informacion
    ''' </summary>
    ''' <param name="SQL_QUERY">Sentencia SQL a ejecutar</param>
    ''' <returns>Datatable con el resultado de la sentencia SQL</returns>
    ''' <remarks></remarks>
    Public Function GetDataSet(ByVal SQL_QUERY As String, Optional ByRef rlayer As rLayerHandler = Nothing) As DataTable
        Try
            Conexion = dbConnect()
            Conexion.Open()
            Dim DT As New DataTable
            Dim dtaDatos As New SqlCeDataAdapter(SQL_QUERY, Conexion)
            dtaDatos.Fill(DT)
            Conexion.Close()
            Return DT
        Catch ex As Exception
            MsgBox("Error en la ejecucion:" + SQL_QUERY)
            rlayer.SQL = (ex.Message)
            rlayer.codigo = 100
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Ejecuta una sentencia SQL en la base de datos SQL-CE del dispositivo movil del tipo Update; Insert; Delete.
    ''' </summary>
    ''' <param name="SQL_QUERY"> Sentencia SQL a ejecutar.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetExecute(ByVal SQL_QUERY As String, Optional ByRef rlayer As rLayerHandler = Nothing) As Integer
        Try
            Dim resultado_transaccion As Integer
            Conexion = dbConnect()
            Conexion.Open()
            Dim CMD As New SqlCeCommand(SQL_QUERY, Conexion)
            CMD.CommandType = CommandType.Text
            resultado_transaccion = CMD.ExecuteNonQuery()
            Conexion.Close()
            Return resultado_transaccion
        Catch ex As Exception
            MsgBox("Error en la ejecucion:" + ex.Message)
            rlayer.SQL = (ex.Message & " : " & SQL_QUERY)
            rlayer.codigo = 100
        End Try

    End Function

    Public Function ExecuteIdentity(ByVal SQL_QUERY As String, ByVal sconexion As SqlCeConnection) As DataTable
        Try
            Dim CMD As New SqlCeCommand(SQL_QUERY, sconexion)
            Dim DT As New DataTable
            Dim dtaDatos As New SqlCeDataAdapter(SQL_QUERY, sconexion)
            dtaDatos.Fill(DT)
            Return DT
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Nothing
    End Function
#End Region



End Class
