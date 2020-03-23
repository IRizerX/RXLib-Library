Imports System.Net
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Text
Imports RXLib.Hashes
Imports RXLib.rgx
Imports RXLib.Headers
Imports RXLib.Posts
Imports System.Security.Cryptography
Public Class RXLib_Android
    Private Shared Cookies As String

    'RXLib v2.4
#Region "Added By Ahmed Al-Jabari - https://www.instagram.com/De4dot"
    'Phone Http :
    Public Function UnFollow(Username As String) As String
        Try
            If (Cookies.Length = 0) Then


            Else
                Dim Account_id As String = get_userid(Username)
                Dim MUId As String = Regex.Match(Cookies, "ds_user_id=(.*?);").Groups(1).Value
                Dim Csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim Mid As String = Regex.Match(Cookies, "mid=(.*?);").Groups(1).Value
                Dim Guiid As String = Guid.NewGuid().ToString()

                Dim POST As String = "signed_body=e7b338527323f723fbb1c7ce1946d69e6a0b470e51f2961b604e4a354bd949bd.{""_csrftoken"":""" & Csrftoken & """,""user_id"":""" & Account_id & """,""radio_type"":""wifi-none"",""_uid"":""" & MUId & """,""device_id"":""android-" & Guiid.Split("-")(4) & """,""_uuid"":""" & Guiid & """}&ig_sig_key_version=4"
                Dim Bytes As Byte() = Encoding.UTF8.GetBytes(POST)
                Dim req As HttpWebRequest = DirectCast(WebRequest.Create("https://i.instagram.com/api/v1/friendships/destroy/" & Account_id & "/"), HttpWebRequest)

                With req
                    .Method = "POST"
                    .Host = "i.instagram.com"
                    .UserAgent = Phon_users_agent
                    .Accept = "*/*"
                    .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
                    .Headers.Add("Accept-Language", "en-US;q=1")
                    .Headers.Add("X-IG-Device-ID", Guiid)
                    .Headers.Add("X-IG-Capabilities", "3brTvwM=")
                    .Headers.Add("X-IG-Connection-Type", "WiFi")
                    .Headers.Add("Cookie", Cookies)
                    .Headers.Add("X-MID", Mid)
                    .AutomaticDecompression = (DecompressionMethods.Deflate Or DecompressionMethods.GZip)
                    .ContentLength = Bytes.Length
                End With

                Dim Stream As Stream = req.GetRequestStream()
                Stream.Write(Bytes, 0, Bytes.Length)
                Stream.Close()
                Dim HttpWebResponse As HttpWebResponse = DirectCast(req.GetResponse(), HttpWebResponse)
                Dim Reader As New StreamReader(HttpWebResponse.GetResponseStream())
                Dim Text As String = Reader.ReadToEnd
                Return Text
            End If
        Catch ex As WebException
            Return "Error : " & (New StreamReader(ex.Response.GetResponseStream()).ReadToEnd())
        End Try
    End Function
    Public Function Follow(Username As String) As String
        Try
            If (Cookies.Length = 0) Then


            Else
                Dim Account_id As String = get_userid(Username)
                Dim MUId As String = Regex.Match(Cookies, "ds_user_id=(.*?);").Groups(1).Value
                Dim Csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim Mid As String = Regex.Match(Cookies, "mid=(.*?);").Groups(1).Value
                Dim Guiid As String = Guid.NewGuid().ToString()

                Dim POST As String = "signed_body=3828a697882139f01b8b9640ef479d005f3216368be0b38bcb0fb3389f62d784.{""_csrftoken"":""" & Csrftoken & """,""user_id"":""" & Account_id & """,""radio_type"":""wifi-none"",""_uid"":""" & MUId & """,""device_id"":""android-" & Guiid.Split("-")(4) & """,""_uuid"":""" & Guiid & """}&ig_sig_key_version=4"
                Dim Bytes As Byte() = Encoding.UTF8.GetBytes(POST)
                Dim req As HttpWebRequest = DirectCast(WebRequest.Create("https://i.instagram.com/api/v1/friendships/create/" & Account_id & "/"), HttpWebRequest)

                With req
                    .Method = "POST"
                    .Host = "i.instagram.com"
                    .UserAgent = Phon_users_agent
                    .Accept = "*/*"
                    .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
                    .Headers.Add("Accept-Language", "en-US;q=1")
                    .Headers.Add("X-IG-Device-ID", Guiid)
                    .Headers.Add("X-IG-Capabilities", "3brTvwM=")
                    .Headers.Add("X-IG-Connection-Type", "WiFi")
                    .Headers.Add("Cookie", Cookies)
                    .Headers.Add("X-MID", Mid)
                    .AutomaticDecompression = (DecompressionMethods.Deflate Or DecompressionMethods.GZip)
                    .ContentLength = Bytes.Length
                End With

                Dim Stream As Stream = req.GetRequestStream()
                Stream.Write(Bytes, 0, Bytes.Length)
                Stream.Close()
                Dim HttpWebResponse As HttpWebResponse = DirectCast(req.GetResponse(), HttpWebResponse)
                Dim Reader As New StreamReader(HttpWebResponse.GetResponseStream())
                Dim Text As String = Reader.ReadToEnd
                Return Text
            End If
        Catch ex As WebException
            Return "Error : " & (New StreamReader(ex.Response.GetResponseStream()).ReadToEnd())
        End Try
    End Function
    Public Function Comment_PostId(Username As String, Post_ID As String, comm As String) As String
        Try
            If (Cookies.Length = 0) Then


            Else
                Dim Account_id As String = get_userid(Username)
            Dim MUId As String = Regex.Match(Cookies, "ds_user_id=(.*?);").Groups(1).Value
            Dim Csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
            Dim Mid As String = Regex.Match(Cookies, "mid=(.*?);").Groups(1).Value
            Dim Guiid As String = Guid.NewGuid().ToString()

            Dim POST As String = "signed_body=a97fcebd77aa28ea73a980b6a07664f6334d8d68a9034c8027d445d1f15a0e54.{""inventory_source"":""media_or_ad"",""idempotence_token"":""" & Guiid & """,""carousel_index"":""0"",""_csrftoken"":""" & Csrftoken & """,""radio_type"":""wifi-none"",""_uid"":""" & MUId & """,""_uuid"":""" & Guiid & """,""comment_text"":""" & comm & """,""is_carousel_bumped_post"":""false"",""container_module"":""comments_v2_feed_contextual_profile"",""feed_position"":""0""}&ig_sig_key_version=4"
            Dim Bytes As Byte() = Encoding.UTF8.GetBytes(POST)

            Dim req As HttpWebRequest = DirectCast(WebRequest.Create("https://i.instagram.com/api/v1/media/" & Post_ID & "_" & Account_id & "/comment/"), HttpWebRequest)
            With req
                .Method = "POST"
                .Host = "i.instagram.com"
                .UserAgent = Phon_users_agent
                .Accept = "*/*"
                .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
                .Headers.Add("Accept-Language", "en-US;q=1")
                .Headers.Add("X-IG-Device-ID", Guiid)
                .Headers.Add("X-IG-Capabilities", "3brTvwM=")
                .Headers.Add("X-IG-Connection-Type", "WiFi")
                .Headers.Add("Cookie", Cookies)
                .Headers.Add("X-MID", Mid)
                .AutomaticDecompression = (DecompressionMethods.Deflate Or DecompressionMethods.GZip)
                .ContentLength = Bytes.Length
            End With

            Dim Stream As Stream = req.GetRequestStream()
            Stream.Write(Bytes, 0, Bytes.Length)
            Stream.Close()
            Dim HttpWebResponse As HttpWebResponse = DirectCast(req.GetResponse(), HttpWebResponse)
            Dim Reader As New StreamReader(HttpWebResponse.GetResponseStream())
            Dim Text As String = Reader.ReadToEnd
                Return Text
                End If
        Catch ex As WebException
            Return "Error : " & (New StreamReader(ex.Response.GetResponseStream()).ReadToEnd())
        End Try
    End Function
    Public Function Unlike_PostId(Username As String, Post_ID As String) As String
        Try
            If (Cookies.Length = 0) Then


            Else
                Dim Account_id As String = get_userid(Username)
                Dim MUId As String = Regex.Match(Cookies, "ds_user_id=(.*?);").Groups(1).Value
                Dim Csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim Mid As String = Regex.Match(Cookies, "mid=(.*?);").Groups(1).Value
                Dim Guiid As String = Guid.NewGuid().ToString()

                Dim POST As String = "signed_body=7ffc667f8f84c1bbca0693bc6941269a4cd31dc0c088539d9ad252b28185907a.{""inventory_source"":""media_or_ad"",""media_id"":""" & Post_ID & "_" & Account_id & """,""_csrftoken"":""" & Csrftoken & """,""radio_type"":""wifi-none"",""_uid"":""" & MUId & """,""_uuid"":""" & Guiid & """,""is_carousel_bumped_post"":""false"",""container_module"":""feed_timeline"",""feed_position"":""1""}&ig_sig_key_version=4&d=0"
                Dim Bytes As Byte() = Encoding.UTF8.GetBytes(POST)
                Dim req As HttpWebRequest = DirectCast(WebRequest.Create("https://i.instagram.com/api/v1/media/" & Post_ID & "_" & Account_id & "/unlike/"), HttpWebRequest)
                With req
                    .Method = "POST"
                    .Host = "i.instagram.com"
                    .UserAgent = Phon_users_agent
                    .Accept = "*/*"
                    .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
                    .Headers.Add("Accept-Language", "en-US;q=1")
                    .Headers.Add("X-IG-Device-ID", Guiid)
                    .Headers.Add("X-IG-Capabilities", "3brTvwM=")
                    .Headers.Add("X-IG-Connection-Type", "WiFi")
                    .Headers.Add("Cookie", Cookies)
                    .Headers.Add("X-MID", Mid)
                    .AutomaticDecompression = (DecompressionMethods.Deflate Or DecompressionMethods.GZip)
                    .ContentLength = Bytes.Length
                End With
                Dim Stream As Stream = req.GetRequestStream()
                Stream.Write(Bytes, 0, Bytes.Length)
                Stream.Close()
                Dim HttpWebResponse As HttpWebResponse = DirectCast(req.GetResponse(), HttpWebResponse)
                Dim Reader As New StreamReader(HttpWebResponse.GetResponseStream())
                Dim Text As String = Reader.ReadToEnd
                Return Text

            End If
        Catch ex As WebException
            Return "Error : " & (New StreamReader(ex.Response.GetResponseStream()).ReadToEnd())
        End Try
    End Function
    Public Function Like_PostId(Username As String, Post_ID As String) As String
        Try
            If (Cookies.Length = 0) Then


            Else
                Dim Account_id As String = get_userid(Username)
                Dim MUId As String = Regex.Match(Cookies, "ds_user_id=(.*?);").Groups(1).Value
                Dim Csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim Mid As String = Regex.Match(Cookies, "mid=(.*?);").Groups(1).Value
                Dim Guiid As String = Guid.NewGuid().ToString()

                Dim POST As String = "signed_body=7ffc667f8f84c1bbca0693bc6941269a4cd31dc0c088539d9ad252b28185907a.{""inventory_source"":""media_or_ad"",""media_id"":""" & Post_ID & "_" & Account_id & """,""_csrftoken"":""" & Csrftoken & """,""radio_type"":""wifi-none"",""_uid"":""" & MUId & """,""_uuid"":""" & Guiid & """,""is_carousel_bumped_post"":""false"",""container_module"":""feed_timeline"",""feed_position"":""1""}&ig_sig_key_version=4&d=0"
                Dim Bytes As Byte() = Encoding.UTF8.GetBytes(POST)
                Dim req As HttpWebRequest = DirectCast(WebRequest.Create("https://i.instagram.com/api/v1/media/" & Post_ID & "_" & Account_id & "/like/"), HttpWebRequest)

                With req
                    .Method = "POST"
                    .Host = "i.instagram.com"
                    .UserAgent = Phon_users_agent
                    .Accept = "*/*"
                    .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
                    .Headers.Add("Accept-Language", "en-US;q=1")
                    .Headers.Add("X-IG-Device-ID", Guiid)
                    .Headers.Add("X-IG-Capabilities", "3brTvwM=")
                    .Headers.Add("X-IG-Connection-Type", "WiFi")
                    .Headers.Add("Cookie", Cookies)
                    .Headers.Add("X-MID", Mid)
                    .AutomaticDecompression = (DecompressionMethods.Deflate Or DecompressionMethods.GZip)
                    .ContentLength = Bytes.Length
                End With

                Dim Stream As Stream = req.GetRequestStream()
                Stream.Write(Bytes, 0, Bytes.Length)
                Stream.Close()
                Dim HttpWebResponse As HttpWebResponse = DirectCast(req.GetResponse(), HttpWebResponse)
                Dim Reader As New StreamReader(HttpWebResponse.GetResponseStream())
                Dim Text As String = Reader.ReadToEnd
                Return Text

            End If
        Catch ex As WebException
            Return "Error : " & (New StreamReader(ex.Response.GetResponseStream()).ReadToEnd())
        End Try
    End Function
    Public Function Login(Username As String, Password As String) As String
        Try
            Dim Csrftoken As String = get_token()
            Dim Mid As String = Get_Mid()
            Dim Guiid As String = Guid.NewGuid().ToString()

            Dim POST As String = "signed_body=8e496c87a09d5e922f6e33df3f399ce298ddbd6f7d6d038417047cc6474a3542.{""id"":""" & Guid.NewGuid().ToString() & """,""_csrftoken"":""" & Csrftoken & """,""username"":""" & Username & """,""guid"":""" + Guiid & """,""device_id"":""android-" & Guiid.Split("-")(4) & """,""password"":""" & Password & """,""login_attempt_count"":""0""}&ig_sig_key_version=4"
            Dim Bytes As Byte() = Encoding.UTF8.GetBytes(POST)
            Dim req As HttpWebRequest = DirectCast(WebRequest.Create("https://i.instagram.com/api/v1/accounts/login/"), HttpWebRequest)

            With req
                .Method = "POST"
                .Host = "i.instagram.com"
                .UserAgent = Phon_users_agent
                .Accept = "*/*"
                .ContentType = "application/x-www-form-urlencoded; charset=UTF-8"
                .Headers.Add("Accept-Language", "en-US;q=1")
                .Headers.Add("X-IG-Device-ID", Guiid)
                .Headers.Add("X-IG-Capabilities", "3brTvwM=")
                .Headers.Add("X-IG-Connection-Type", "WiFi")
                .Headers.Add("Cookie", "mid=" & Mid & "; csrftoken=" & Csrftoken & "; rur=ASH")
                .Headers.Add("X-MID", Mid)
                .ContentLength = Bytes.Length
            End With

            Dim Stream As Stream = req.GetRequestStream()
            Stream.Write(Bytes, 0, Bytes.Length)
            Stream.Close()
            Dim HttpWebResponse As HttpWebResponse = DirectCast(req.GetResponse(), HttpWebResponse)
            Dim Reader As New StreamReader(HttpWebResponse.GetResponseStream())
            Dim Text As String = Reader.ReadToEnd

            Dim c As String = HttpWebResponse.Headers(HttpResponseHeader.SetCookie).ToString
            Cookies = Regex.Match(c, "csrftoken=.*?;").Value & " " & Regex.Match(c, "mid=.*?;").Value & " " & Regex.Match(c, "ds_user_id=(.*?);").Groups(0).Value.ToString() & " " + Regex.Match(c, "sessionid=(.*?);").Groups(0).Value.ToString()

            If Text.Contains("status"": ""ok") Then
                Return "Login done"
            Else
                Return Text
            End If

        Catch ex As WebException
            MsgBox(New StreamReader(ex.Response.GetResponseStream()).ReadToEnd())
        End Try
    End Function
    Public Function GetFollowersFromSearch(Word As String) As List(Of String)
        Try
            If (Cookies.Length = 0) Then


            Else
                Dim users As New List(Of String)
                Dim req As HttpWebRequest = DirectCast(WebRequest.Create("https://i.instagram.com/api/v1/fbsearch/topsearch/?query=" & Word), HttpWebRequest)
                req.Method = "GET"
                req.UserAgent = Phon_users_agent
                req.Accept = "*/*"
                req.Headers.Add("Accept-Language", "en-US;q=1")
                req.Headers.Add("Cookie", Cookies)
                req.Headers.Add("X-Ads-Opt-Out", "0")
                req.Headers.Add("X-FB", "0")
                req.Headers.Add("X-IG-Capabilities", "3w==")
                req.Headers.Add("X-IG-Connection-Type", "WiFi")
                Dim res As HttpWebResponse = DirectCast(req.GetResponse(), HttpWebResponse)
                Dim st1 As Stream = res.GetResponseStream
                Dim str As String = New StreamReader(st1).ReadToEnd
                Dim matches As MatchCollection = Regex.Matches(str, "username"": ""(.+?)""")
                For Each match As Match In matches
                    users.Add(match.Groups(1).Value)
                Next
                Return users
            End If
        Catch ex As Exception
            Dim errors As New List(Of String)
            Return errors
        End Try
    End Function
    Public Function Get_EmailBusiness(Username As String) As String
        Try
            If (Cookies.Length = 0) Then


            Else
                Dim id As String = get_userid(Username)
                Dim Request As HttpWebRequest = WebRequest.Create("https://i.instagram.com/api/v1/users/" & id & "/info/")

                With Request
                    .Method = "GET"
                    .UserAgent = Phon_users_agent
                    .Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"
                    .Headers.Add("Accept-Language", "en-US")
                    .Headers.Add("Cookie", Cookies)
                End With

                Dim Response As HttpWebResponse = Request.GetResponse()
                Dim Streams As Stream = Response.GetResponseStream()
                Dim Res As String = New StreamReader(Streams).ReadToEnd
                Dim EmailOrPhone As String = Regex.Match(Res, "public_email"": ""(.+?)""").Groups(1).Value
                If String.IsNullOrWhiteSpace(EmailOrPhone) Then
                    Return "null"
                Else
                    Return EmailOrPhone
                End If
            End If
        Catch ex As Exception
            Return "Error : " & ex.Message
        End Try
    End Function

    'Params
    Private Function Get_Mid() As String
        Try
            Dim PP As HttpWebRequest = DirectCast(WebRequest.Create("https://www.instagram.com/web/__mid/"), HttpWebRequest)
            PP.Method = "GET"
            PP.UserAgent = user_agent
            Dim GResponse As HttpWebResponse = DirectCast(PP.GetResponse(), HttpWebResponse)
            Dim GReader As New StreamReader(GResponse.GetResponseStream())
            Dim Mid As String = GReader.ReadToEnd
            If String.IsNullOrWhiteSpace(Mid) Then
                Return "null"
            Else
                Return Mid
            End If
        Catch ex As Exception
            Return "Error : " & ex.Message
        End Try
    End Function
    Private Function Get_Ajax() As String
        Try
            Dim PP As HttpWebRequest = DirectCast(WebRequest.Create("https://www.instagram.com/"), HttpWebRequest)
            PP.Method = "GET"
            PP.UserAgent = user_agent
            Dim GResponse As HttpWebResponse = DirectCast(PP.GetResponse(), HttpWebResponse)
            Dim GReader As New StreamReader(GResponse.GetResponseStream())
            Dim Res As String = GReader.ReadToEnd
            Dim Ajax As String = Regex.Match(Res, "rollout_hash"":""(\w+)""").Groups(1).Value
            If String.IsNullOrWhiteSpace(Ajax) Then
                Return "null"
            Else
                Return Ajax
            End If
        Catch ex As Exception
            Return "Error : " & ex.Message
        End Try
    End Function



#End Region


#Region "Old Function"
    'Old Function
    Private Function get_userid(Username As String) As String
        Try
            Dim httpGet = DirectCast(WebRequest.Create("https://www.instagram.com/" & Username), HttpWebRequest)
            httpGet.Method = "GET"
            httpGet.ContentType = "application/json"
            httpGet.Headers.Add("X-Instagram-AJAX", "1")
            httpGet.
            Headers.Add("x-requested-with", "XMLHttpRequest")

            Dim Get_Response As HttpWebResponse
            Get_Response = DirectCast(httpGet.GetResponse(), HttpWebResponse)

            Dim Get_Reader As New StreamReader(Get_Response.GetResponseStream())

            Return Regex.Match(Get_Reader.ReadToEnd, """profilePage_(\d+)"",").Groups(1).Value
        Catch ex As Exception
            Return ""
        End Try

    End Function


    'Old Function
    Private Function get_token() As String
        Try
            Dim httpGet = DirectCast(WebRequest.Create("https://www.instagram.com/"), HttpWebRequest)
            httpGet.Method = "GET"
            httpGet.ContentType = ""
            httpGet.Headers.Add("X-Instagram-AJAX", "1")
            httpGet.Headers.Add("x-requested-with", "XMLHttpRequest")
            Dim Get_Response As HttpWebResponse
            Get_Response = DirectCast(httpGet.GetResponse(), HttpWebResponse)
            Dim Get_Reader As New StreamReader(Get_Response.GetResponseStream())
            Return Regex.Match(Get_Reader.ReadToEnd, """csrf_token"":""(\w+)""").Groups(1).Value
        Catch ex As Exception
            Return ""
        End Try
    End Function
#End Region
End Class


