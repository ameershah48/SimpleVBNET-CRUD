Public Class Form1
    Dim position

    Public Function getData()
        Me.TableTableAdapter.Fill(Me.TestDataSet.table)
    End Function

    Public Function getCount()
        Dim rows = Me.TestDataSet.table.Count
        Return rows
    End Function

    Public Function emptyField()
        txtName.Text = ""
        txtPhone.Text = ""
        txtSearch.Text = ""
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getData()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim name = txtName.Text
        Dim phone = txtPhone.Text

        If (name = "" Or phone = "") Then
            MessageBox.Show("Please fill the blanks!")
        Else
            Me.TableTableAdapter.Insert(name:=name, phone:=phone)
        End If

        getData()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        If position >= getCount() Or position < 0 Then
            MessageBox.Show("Data is currently empty, please add new data")
        Else
            Me.TestDataSet.table.Rows(position)("name") = txtName.Text
            Me.TestDataSet.table.Rows(position)("phone") = txtPhone.Text
            Me.TableTableAdapter.Update(Me.TestDataSet.table)
            MessageBox.Show("Data Updated")
        End If

        getData()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If position >= getCount() Or position < 0 Then
            MessageBox.Show("Data is currently empty, please add new data")
        Else
            Me.TestDataSet.table.Rows(position).Delete()
            Me.TableTableAdapter.Update(Me.TestDataSet.table)
            MessageBox.Show("Data has been deleted")
            position = -1
        End If

        emptyField()
        getData()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        emptyField()
        getData()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        position = e.RowIndex

        If position >= getCount() Or position < 0 Then
            MessageBox.Show("Data is currently empty, please add new data")
        Else
            txtName.Text = Me.TestDataSet.table.Rows(position)("name")
            txtPhone.Text = Me.TestDataSet.table.Rows(position)("phone")
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Me.TableBindingSource.Filter = "name LIKE '" & txtSearch.Text & "%' or phone LIKE '" & txtSearch.Text & "%'"
    End Sub
End Class
