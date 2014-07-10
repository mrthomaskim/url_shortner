#Region " Imports "

Imports System.Data
Imports System.Data.SqlClient

#End Region

Public Class DBOperations

#Region " Variable Declarations "

#Region " Public Properties "

	Private _Err As Exception = Nothing
	Public ReadOnly Property Err As Exception
		Get
			Return _Err
		End Get
	End Property

	Private _LastInsertID As Int32 = -1
	Public ReadOnly Property LastInsertID As Int32
		Get
			Return _LastInsertID
		End Get
	End Property

#End Region

	Dim ConnectString As String = ""

#End Region

#Region " Initializations "

  Public Sub New(Optional ByVal IsDev As Boolean = False)
        'ConnectString = "Data Source=JNT101;Initial Catalog=CWSI;User ID=cwsiUser;Password=QaZ13579;pooling=false;"
        'ConnectString = "Server=.\SQLExpress;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\URLShortner.mdf;Database=DB_25972_cwsidb; Persist Security Info=True;User ID=DB_25972_cwsidb_user;Password=P@ssword1"
        ' ConnectString = "Server=.\SQLExpress;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\URLShortner.mdf;Database=URLShortner;Persist Security Info=True;User ID=DB_25972_cwsidb_user;Password=P@ssword1"
        ' ConnectString = "Server=.\SQLExpress;AttachDbFilename=|DataDirectory|URLShortner.mdf; Database=DB_25972_cwsidb;Persist Security Info=True;User ID=DB_25972_cwsidb_user;Password=P@ssword1;"
        ConnectString = "Data Source=.\SQLExpress;Initial Catalog=url_shortner_db;Persist Security Info=True;User Id=svc_url_dude;Password=Terminator123;"
  End Sub

#End Region

#Region " Public Methods "

	''' <summary>
	''' This function is called to perform any Add, update or delete operation on SQL Server using a stored procedure.
	''' </summary>
	''' <param name="SPName">Name of the stored procedure to be called.</param>
	''' <param name="Parameters">A string array of parameters in the same order as the stored procedure is expecting its parameters.</param>
	''' <returns>True/False.</returns>
	''' <remarks>This is a wrapper method to be called throughout the program whereever an add, update or delete operation needs to be performed. If there is an add operation, it will also get the LastInsertedID() and store it in the readonly public property LastInsertedID which is accessible via the DBOperations object after this function returns successfully to the calling code.</remarks>
	Public Function AddUpdateDelete(ByRef SPName As String, ByRef Parameters() As String) As Boolean
		_LastInsertID = -1
        Try
            Using oCon As New System.Data.SqlClient.SqlConnection(ConnectString)
                oCon.Open()
                Using oCommand As New SqlCommand(SPName, oCon)
                    If Not SetParameters(oCommand, Parameters) Then Throw _Err
                    _LastInsertID = oCommand.ExecuteScalar
                    Return True
                End Using
            End Using
        Catch ex As Exception
            _Err = ex
            Return False
        End Try
	End Function

	''' <summary>
	''' This function is called to perform any select operation on SQL Server using a stored procedure.
	''' </summary>
	''' <param name="SPName">Name of the stored procedure to be called.</param>
	''' <param name="Parameters">A string array of parameters in the same order as the stored procedure is expecting its parameters.</param>
	''' <returns>Resultant data as DataTable.</returns>
	''' <remarks>It will perform the select operation via the provided stored procedure and the returned data by SQL Server is forwarded to the calling code as a datatable for further manipulation. In case of a single row return, the datatable only contains one row and it is accessible using Datatable.Rows(0) construction.</remarks>
    Public Overloads Function GetData(ByRef SPName As String, ByRef Parameters() As String) As DataTable
        Dim RetTable As New DataTable

        Try
            Using oCon As New System.Data.SqlClient.SqlConnection(ConnectString)
                oCon.Open()
                Using oCommand As New SqlCommand(SPName, oCon)
                    If Not SetParameters(oCommand, Parameters) Then Throw _Err
                    Using oReader As SqlDataReader = oCommand.ExecuteReader
                        RetTable.Load(oReader)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            _Err = ex
        End Try
        Return RetTable
    End Function
    Public Overloads Function GetData(ByRef Query As String) As DataTable
        Dim RetTable As New DataTable

        Try
            Using oCon As New System.Data.SqlClient.SqlConnection(ConnectString)
                oCon.Open()
                Using oCommand As New SqlCommand(Query, oCon)
                    ' If Not SetParameters(oCommand, Parameters) Then Throw _Err
                    Using oReader As SqlDataReader = oCommand.ExecuteReader
                        RetTable.Load(oReader)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            _Err = ex
        End Try
        Return RetTable
    End Function
    ''' <summary>
    ''' This function is called to perform a Select count(*) operation on SQL Server using a stored procedure.
    ''' </summary>
    ''' <param name="SPName">Name of the stored procedure to be called.</param>
    ''' <param name="Parameters">A string array of parameters in the same order as the stored procedure is expecting its parameters.</param>
    ''' <returns>Count of the rows as Integer.</returns>
    ''' <remarks>This method is useful when we need to check the existance of a specific data in database for further insert, update or delete operation.</remarks>
	Public Function GetCount(ByRef SPName As String, ByRef Parameters() As String) As Int32
		Try
			Using oCon As New System.Data.SqlClient.SqlConnection(ConnectString)
				oCon.Open()
				Using oCommand As New SqlCommand(SPName, oCon)
					If Not SetParameters(oCommand, Parameters) Then Throw _Err
                    Return oCommand.ExecuteScalar
				End Using
			End Using
		Catch ex As Exception
			_Err = ex
			Return -1
		End Try
	End Function
   
    Public Function GetLookups(ByRef SPName As String, ByVal Parameters() As String) As String
        Dim RetValue As String = ""

        Try
            Using oCon As New System.Data.SqlClient.SqlConnection(ConnectString)
                oCon.Open()
                Using oCommand As New SqlCommand(SPName, oCon)
                    If Not SetParameters(oCommand, Parameters) Then Throw _Err
                    Using oReader As SqlDataReader = oCommand.ExecuteReader
                        If Not oReader.HasRows Then Throw New Exception("No matching data was found in database.")
                        While oReader.Read
                            If RetValue.Length > 0 Then RetValue &= vbCrLf
                            RetValue &= String.Format("{0}|{1}", oReader(Parameters(1)).ToString.Trim, oReader(Parameters(2)).ToString.Trim)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            _Err = ex
        End Try
        Return RetValue
    End Function
#End Region

#Region " Helper Functions "

	''' <summary>
	''' This helper method is used to assign the parameter values to command object before executing it against the database.
	''' </summary>
	''' <param name="oCommand">SQL Command object to be executed.</param>
	''' <param name="Parameters">A string array of parameters in the same order as the stored procedure is expecting its parameters.</param>
	''' <returns>True/False.</returns>
	''' <remarks>This method calls the DeriveParameters method on the passed in command object to retrieve the list of parameters being expected by the stored procedure. Then each Input or InputOutput type parameter is assigned a value from incoming string array. Here the point to note is the order of parameter values in input array is the same as the order of parameters defined in stored procedure. It is very important otherwise this process will fail.</remarks>
	Private Function SetParameters(ByRef oCommand As SqlCommand, ByRef Parameters() As String) As Boolean
		Try
			Dim idx As Integer
			oCommand.CommandType = Data.CommandType.StoredProcedure
			oCommand.Parameters.Clear()
			SqlCommandBuilder.DeriveParameters(oCommand)
			idx = 0

			For Each Param As SqlParameter In oCommand.Parameters
				If Param.Direction = Data.ParameterDirection.Input Or Param.Direction = Data.ParameterDirection.InputOutput Then
					If Param.DbType = Data.DbType.Boolean Then
                        If Parameters(idx) = True Then
                            Param.Value = 1
                        Else
                            Param.Value = 0
                        End If
					Else
                        If Not String.IsNullOrEmpty(Parameters(idx)) Then
                            Param.Value = Parameters(idx)
                        Else
                            Param.Value = DBNull.Value
                        End If
					End If
					idx += 1
				End If
			Next

			Return True
		Catch ex As Exception
			_Err = ex
			Return False
		End Try
	End Function

#End Region

End Class
