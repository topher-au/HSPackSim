Imports System.IO

Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class PackSimulator
    Public Class CardData
        Public CardList As New List(Of Card)
        Public ReadOnly Property Commons As List(Of Card)
            Get
                Return CardList.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.COMMON)
            End Get
        End Property
        Public ReadOnly Property Rares As List(Of Card)
            Get
                Return CardList.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.RARE)
            End Get
        End Property
        Public ReadOnly Property Epics As List(Of Card)
            Get
                Return CardList.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.EPIC)
            End Get
        End Property
        Public ReadOnly Property Legendarys As List(Of Card)
            Get
                Return CardList.FindAll(Function(x) x.Rarity = Enums.CARD_RARITY.LEGENDARY)
            End Get
        End Property
        Public Sub LoadFromJson(JsonFile As String)
            Dim loadJson As New StreamReader(JsonFile)
            Dim jsonData As String = loadJson.ReadToEnd

            Dim objJsonSets As JObject = JsonConvert.DeserializeObject(jsonData)

            For Each o In objJsonSets ' Recurse through all sets
                For Each card In o.Value.Children ' Recurse through each card in each set
                    If Not IsNothing(card.Item("collectible")) Then
                        If (card.Item("type").Value(Of String) = "Minion" Or
                            card.Item("type").Value(Of String) = "Spell" Or
                            card.Item("type").Value(Of String) = "Weapon") And
                            card.Item("collectible").Value(Of Boolean) = True Then
                            Select Case o.Key
                                Case "Basic"
                                    CardList.Add(New Card(Enums.CARD_SETS.BASIC, card))
                                Case "Blackrock Mountain"
                                    CardList.Add(New Card(Enums.CARD_SETS.BRM, card))
                                Case "Classic"
                                    CardList.Add(New Card(Enums.CARD_SETS.CLASSIC, card))
                                Case "Curse of Naxxramas"
                                    CardList.Add(New Card(Enums.CARD_SETS.NAXX, card))
                                Case "Goblins vs Gnomes"
                                    CardList.Add(New Card(Enums.CARD_SETS.GVG, card))
                                Case "Promotion"
                                    CardList.Add(New Card(Enums.CARD_SETS.PROMO, card))
                                Case "Reward"
                                    CardList.Add(New Card(Enums.CARD_SETS.REWARD, card))
                                Case "The Grand Tournament"
                                    CardList.Add(New Card(Enums.CARD_SETS.TGT, card))
                                Case Else
                                    MsgBox("Found unknown set: " & o.Key.ToString)
                                    End
                            End Select
                        End If
                    End If
                Next
            Next
        End Sub
    End Class
    Public Class Generator
        Public Cards As New CardData
        Private random As New Random(CInt(Date.Now.Ticks And &H0000FFFF))
        Public Function RandomPack(Optional PackType As Enums.CARD_SETS = Enums.CARD_SETS.CLASSIC) As List(Of Card)

            Dim cardPack As New List(Of Card)
            Dim gotRare As Boolean = False
            For i = 1 To 5
                Dim rndCard = RandomCard(PackType)
                cardPack.Add(rndCard)
                If rndCard.Rarity >= Enums.CARD_RARITY.RARE Then _
                    gotRare = True
            Next

            If gotRare = False Then
                Dim upgradeCard As Integer = random.NextDouble() * 4
                Do
                    Dim rndCard = RandomCard(PackType)
                    If rndCard.Rarity = Enums.CARD_RARITY.COMMON Then Continue Do
                    cardPack.Item(upgradeCard) = rndCard
                    Exit Do
                Loop
            End If

            Return cardPack

        End Function
        Public Function RandomCards(Count As Integer, Optional PackType As Enums.CARD_SETS = Enums.CARD_SETS.CLASSIC) As List(Of Card)
            If Count < 1 Then Count = 1

            Dim cardList As New List(Of Card)
            For i = 1 To Count
                cardList.Add(RandomCard(PackType))
            Next

            Return cardList

        End Function
        Public Function RandomCard(Optional Packtype As Enums.CARD_SETS = Enums.CARD_SETS.CLASSIC)
            Dim cardChance As Double = random.NextDouble

            ' We start with a common card, and it has a chance to get "upgraded" by one rarity each
            ' time we roll. There is approx. a 1 in 5 chance of it being upgraded

            Dim cardRarity As Enums.CARD_RARITY = Enums.CARD_RARITY.COMMON
            Do Until cardChance >= (1 / 5) Or cardRarity = Enums.CARD_RARITY.LEGENDARY
                cardRarity += 1                         'Upgrade to next rarity
                cardChance = random.NextDouble          'Reroll for another upgrade
            Loop

            ' After the card's rarity is determined, we next will roll for whether it is NORMAL or GOLDEN
            ' and then we will determine which card it will be
            Dim eligibleCards As New List(Of Card)
            Dim cardNum As Integer = 0
            Dim cardQuality As Enums.CARD_QUALITY = Enums.CARD_QUALITY.NORMAL
            cardChance = random.NextDouble      'Chance that it will be golden

            Select Case cardRarity
                Case Enums.CARD_RARITY.COMMON                   ' Common cards 1/50 Golden Chance
                    If cardChance < (1 / 50) Then
                        cardQuality = Enums.CARD_QUALITY.GOLDEN
                    End If
                    eligibleCards = Cards.Commons.FindAll(Function(x) x.CardSet = Packtype)
                    cardNum = random.NextDouble() * (eligibleCards.Count - 1)
                Case Enums.CARD_RARITY.RARE                     ' Rare cards 1/20 Golden Chance
                    If cardChance < (1 / 20) Then
                        cardQuality = Enums.CARD_QUALITY.GOLDEN
                    End If
                    eligibleCards = Cards.Rares.FindAll(Function(x) x.CardSet = Packtype)
                    cardNum = random.NextDouble() * (eligibleCards.Count - 1)
                Case Enums.CARD_RARITY.EPIC                     ' Epic cards 1/15 Golden Chance
                    If cardChance < (1 / 15) Then
                        cardQuality = Enums.CARD_QUALITY.GOLDEN
                    End If
                    eligibleCards = Cards.Epics.FindAll(Function(x) x.CardSet = Packtype)
                    cardNum = random.NextDouble() * (eligibleCards.Count - 1)
                Case Enums.CARD_RARITY.LEGENDARY                ' Legendary cards 1/10 Golden Chance
                    If cardChance < (1 / 10) Then
                        cardQuality = Enums.CARD_QUALITY.GOLDEN
                    End If
                    eligibleCards = Cards.Legendarys.FindAll(Function(x) x.CardSet = Packtype)
                    cardNum = random.NextDouble() * (eligibleCards.Count - 1)
            End Select

            Return New Card(Packtype, eligibleCards.Item(cardNum).Token, cardQuality)

        End Function
    End Class
    Public Class Card
        Public ReadOnly Property CardId As String
            Get
                Return cardToken.Item("id")
            End Get
        End Property
        Public ReadOnly Property Name As String
            Get
                Return cardToken.Item("name").ToString
            End Get
        End Property
        Public ReadOnly Property Rarity As Enums.CARD_RARITY
            Get
                Select Case cardToken.Item("rarity").ToString
                    Case "Free"
                        Return Enums.CARD_RARITY.BASIC

                    Case "Common"
                        Return Enums.CARD_RARITY.COMMON

                    Case "Rare"
                        Return Enums.CARD_RARITY.RARE

                    Case "Epic"
                        Return Enums.CARD_RARITY.EPIC

                    Case "Legendary"
                        Return Enums.CARD_RARITY.LEGENDARY
                End Select
                Return Nothing
            End Get
        End Property
        Public ReadOnly Property CardSet As Enums.CARD_SETS
            Get
                Return cardParentSet
            End Get
        End Property
        Public ReadOnly Property Quality As Enums.CARD_QUALITY
            Get
                Return cardQuality
            End Get
        End Property
        Public ReadOnly Property Token As JToken
            Get
                Return cardToken
            End Get
        End Property
        Private cardParentSet As Enums.CARD_SETS
        Private cardToken As JToken
        Private cardQuality As Enums.CARD_QUALITY
        Public Sub New(CardSet As Enums.CARD_SETS, Token As JToken, Optional Quality As Enums.CARD_QUALITY = Enums.CARD_QUALITY.NORMAL)
            cardParentSet = CardSet
            cardToken = Token
            cardQuality = Quality
        End Sub
    End Class
    Public Class Enums
        Enum CARD_SETS
            NONE
            BASIC
            BRM
            CLASSIC
            NAXX
            GVG
            PROMO
            REWARD
            TGT
        End Enum
        Enum CARD_RARITY
            BASIC
            COMMON
            RARE
            EPIC
            LEGENDARY
        End Enum
        Enum CARD_QUALITY
            NORMAL
            GOLDEN
        End Enum
    End Class
End Class
