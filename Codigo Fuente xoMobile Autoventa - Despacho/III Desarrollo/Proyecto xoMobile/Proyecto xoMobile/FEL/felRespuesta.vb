Public Class felRespuesta
    'Declaración de atributos para clase Detalle Factura
    Private _estado As Boolean
    Private _mensaje As String
    Private _respuesta As String

    Public Property ESTADO() As Boolean
        Get
            Return _estado
        End Get
        Set(ByVal value As Boolean)
            _estado = value
        End Set
    End Property


    Public Property MENSAJE() As String
        Get
            Return _mensaje
        End Get
        Set(ByVal value As String)
            _mensaje = value
        End Set
    End Property

    Public Property RESPUESTA() As String
        Get
            Return _respuesta
        End Get
        Set(ByVal value As String)
            _respuesta = value
        End Set
    End Property

End Class
