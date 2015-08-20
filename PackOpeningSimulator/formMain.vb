Imports PackOpeningSimulator.PackSimulator
Imports System.IO
Public Class formMain
    Public PackSim As New Generator

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim simFile As String = String.Format("Standard Sim {0}-{1}-{2} {3}-{4}-{5}.csv", TimeOfDay.Day, TimeOfDay.Month, TimeOfDay.Year, TimeOfDay.Hour, TimeOfDay.Minute, TimeOfDay.Second)
        Dim simCount As Integer = textSimCount.Text 'amount of simulations to run
        Dim simPacks As Integer = textSimPacks.Text ' amount of packs to open
        Dim packType As PackSimulator.Enums.CARD_SETS

        Select Case comboPackType.SelectedIndex
            Case 0
                packType = Enums.CARD_SETS.CLASSIC

            Case 1
                packType = Enums.CARD_SETS.GVG

            Case 2
                packType = Enums.CARD_SETS.TGT
        End Select

        Dim csv As New StreamWriter(simFile)
        csv.WriteLine("Common,Rare,Epic,Legendary,GoldCommon,GoldRare,GoldEpic,GoldLegendary,Extra Dust,")
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = simCount

        For z = 1 To simCount ' run simulations
            Debug.WriteLine("Simulation " & z & " of " & simCount & " (" & simPacks & " packs/simulation)")

            Dim allCards As New List(Of Card)
            For i = 1 To simPacks ' generate card packs
                Dim RandPack As List(Of Card) = PackSim.RandomPack(Enums.CARD_SETS.TGT)
                For Each c In RandPack
                    allCards.Add(c) ' add all cards to final list
                Next
            Next

            Dim dust As Integer = 0

            Dim excessCards As New List(Of Card)
            For Each card In allCards
                Dim allSame As List(Of Card) = allCards.FindAll(Function(x) (x.CardId = card.CardId) And (x.Quality = card.Quality))
                If (allSame.Count > 2) And Not (excessCards.FindAll(Function(x) (x.CardId = card.CardId) And (x.Quality = card.Quality)).Count > 0) Then
                    For x = 1 To (allSame.Count - 2)
                        excessCards.Add(card)
                    Next
                End If
            Next

            For Each c In excessCards ' calculate extra dust
                Dim cardCt = excessCards.FindAll(Function(x) x.CardId = c.CardId)
                Select Case c.Rarity
                    Case Enums.CARD_RARITY.COMMON
                        If c.Quality = Enums.CARD_QUALITY.NORMAL Then
                            dust += 5 * (cardCT.Count - 2)
                        Else
                            dust += 50 * (cardCT.Count - 2)
                        End If
                    Case Enums.CARD_RARITY.RARE
                        If c.Quality = Enums.CARD_QUALITY.NORMAL Then
                            dust += 20 * (cardCT.Count - 2)
                        Else
                            dust += 100 * (cardCT.Count - 2)
                        End If
                    Case Enums.CARD_RARITY.EPIC
                        If c.Quality = Enums.CARD_QUALITY.NORMAL Then
                            dust += 100 * (cardCT.Count - 2)
                        Else
                            dust += 400 * (cardCT.Count - 2)
                        End If
                    Case Enums.CARD_RARITY.LEGENDARY
                        If c.Quality = Enums.CARD_QUALITY.NORMAL Then
                            dust += 400 * (cardCT.Count - 2)
                        Else
                            dust += 1600 * (cardCT.Count - 2)
                        End If
                End Select

                ProgressBar1.Value = z
                ProgressBar1.Update()
            Next
            'write output

            csv.Write(allCards.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.COMMON And x.Quality = Enums.CARD_QUALITY.NORMAL).Count.ToString & ",")
            csv.Write(allCards.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.RARE And x.Quality = Enums.CARD_QUALITY.NORMAL).Count.ToString & ",")
            csv.Write(allCards.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.EPIC And x.Quality = Enums.CARD_QUALITY.NORMAL).Count.ToString & ",")
            csv.Write(allCards.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.LEGENDARY And x.Quality = Enums.CARD_QUALITY.NORMAL).Count.ToString & ",")
            csv.Write(allCards.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.COMMON And x.Quality = Enums.CARD_QUALITY.GOLDEN).Count.ToString & ",")
            csv.Write(allCards.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.RARE And x.Quality = Enums.CARD_QUALITY.GOLDEN).Count.ToString & ",")
            csv.Write(allCards.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.EPIC And x.Quality = Enums.CARD_QUALITY.GOLDEN).Count.ToString & ",")
            csv.Write(allCards.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.LEGENDARY And x.Quality = Enums.CARD_QUALITY.GOLDEN).Count.ToString & ",")
            csv.WriteLine(dust & ",")
        Next

        csv.Flush()
        csv.Dispose()

    End Sub

    Private Sub formMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not My.Computer.FileSystem.FileExists("AllSets.json") Then
            MsgBox("Please download AllSets.json from http://www.hearthstonejson.com and place in the simulator folder.", vbCritical + vbOKOnly, "Hearthstone Pack Simulator")
            End
        End If
        PackSim.Cards.LoadFromJson("AllSets.json")
        comboPackType.SelectedIndex = 2
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim packCount As Integer
        Dim rndPack As List(Of Card)
        Dim gLeg As List(Of Card)
        Dim packType As PackSimulator.Enums.CARD_SETS

        Select Case comboPackType.SelectedIndex
            Case 0
                packType = Enums.CARD_SETS.CLASSIC

            Case 1
                packType = Enums.CARD_SETS.GVG

            Case 2
                packType = Enums.CARD_SETS.TGT
        End Select
        Do
            rndPack = PackSim.RandomPack(packType)
            gLeg = rndPack.FindAll(Function(x)
                                       Return x.Quality = Enums.CARD_QUALITY.GOLDEN And x.Rarity = Enums.CARD_RARITY.LEGENDARY
                                   End Function)
            If gLeg.Count > 0 Then Exit Do
            packCount += 1
        Loop
        MsgBox("Got Golden Legendary after " & packCount.ToString & " packs." & vbNewLine & "It was: " & gLeg.Item(0).Name)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim simFile As String = String.Format("Verbose Sim {0}-{1}-{2}.csv", TimeOfDay.Hour, TimeOfDay.Minute, TimeOfDay.Second)

        Dim simCount As Integer = textSimCount.Text 'amount of simulations to run
        Dim simPacks As Integer = textSimPacks.Text ' amount of packs to open
        Dim packType As PackSimulator.Enums.CARD_SETS

        Select Case comboPackType.SelectedIndex
            Case 0
                packType = Enums.CARD_SETS.CLASSIC

            Case 1
                packType = Enums.CARD_SETS.GVG

            Case 2
                packType = Enums.CARD_SETS.TGT
        End Select

        Dim csv As New StreamWriter(simFile)
        csv.WriteLine("NAME,RARITY,QUALITY,")
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = simCount

        For z = 1 To simCount ' run simulations
            Debug.WriteLine("Simulation " & z & " of " & simCount & " (" & simPacks & " packs/simulation)")

            Dim allCards As New List(Of Card)
            For i = 1 To simPacks ' generate card packs
                Dim RandPack As List(Of Card) = PackSim.RandomPack(Enums.CARD_SETS.TGT)
                For Each c In RandPack
                    allCards.Add(c) ' add all cards to final list
                Next
            Next

            'write output
            For Each c In allCards
                csv.WriteLine("{0},{1},{2},", c.Name, c.Rarity.ToString, c.Quality.ToString)
            Next

            ProgressBar1.Value = z
            ProgressBar1.Update()
        Next

        csv.Flush()
        csv.Dispose()


    End Sub
End Class
