
#Region " Imports "

Imports System.Text

#End Region

Public Class RandomStringGenerator

#Region " Variable Declarations "

#Region " Public Properties "



#End Region

#Region " Local Variables "

  Const Key_Letters As String = "abcdefghijklmnopqrstuvwxyz"
  Const Key_Numbers As String = "0123456789"
  Dim LettersArray As Char()
  Dim NumbersArray As Char()

#End Region

#End Region

#Region " Initializations "

  Public Sub New()
    MyBase.New()
  End Sub

#End Region

#Region " Public Methods "

  Public Function Generate(Optional ByVal MaxLength As Integer = 11) As String
    Dim i_key As Integer
    Dim Random1 As Single
    Dim arrIndex As Int16
    Dim sb As New StringBuilder
    Dim RandomLetter As String

    LettersArray = Key_Letters.ToCharArray
    NumbersArray = Key_Numbers.ToCharArray

    For i_key = 1 To MaxLength
      Randomize()
      Random1 = Rnd()
      arrIndex = -1
      If (CType(Random1 * 111, Integer)) Mod 2 = 0 Then
        Do While arrIndex < 0
          arrIndex = Convert.ToInt16(LettersArray.GetUpperBound(0) * Random1)
        Loop
        RandomLetter = LettersArray(arrIndex)
        If (CType(arrIndex * Random1 * 99, Integer)) Mod 2 <> 0 Then
          RandomLetter = LettersArray(arrIndex).ToString
          RandomLetter = RandomLetter.ToUpper
        End If
        sb.Append(RandomLetter)
      Else
        Do While arrIndex < 0
          arrIndex = _
            Convert.ToInt16(NumbersArray.GetUpperBound(0) _
            * Random1)
        Loop
        sb.Append(NumbersArray(arrIndex))
      End If
    Next
    Return sb.ToString
  End Function

#End Region

#Region " Helper Methods "


#End Region

End Class