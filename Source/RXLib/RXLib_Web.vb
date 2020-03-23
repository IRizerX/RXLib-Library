Imports System.Net
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Text
Imports RXLib.Hashes
Imports RXLib.rgx
Imports RXLib.Headers
Imports RXLib.Posts
Imports System.Security.Cryptography

Public Class RXLib_Web
    Private Shared Cookies As String
    Private firstname, email, user As String
    'RXLib v2.4

#Region "Added By Ahmed Al-Jabari - https://www.instagram.com/De4dot"
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

    'Web Http : 
    Public Function Email_Reset(Username As String)
        Try
            Dim Bytes As Byte() = Encoding.Default.GetBytes("email_or_username=" & Username & "&recaptcha_challenge_field=")
            Dim Request_Rest As HttpWebRequest = DirectCast(WebRequest.Create("https://www.instagram.com/accounts/account_recovery_send_ajax/"), HttpWebRequest)
            With Request_Rest
                .Proxy = Nothing
                .Method = "POST"
                .Host = "www.instagram.com"
                .KeepAlive = True
                .UserAgent = user_agent
                .Accept = "*/*"
                .Referer = ("https://www.instagram.com/")
                .ContentType = ("application/x-www-form-urlencoded")
                .Headers.Add("X-Requested-With", "XMLHttpRequest")
                .Headers.Add("X-IG-App-ID", "936619743392459")
                .Headers.Add("X-Instagram-AJAX", Get_Ajax)
                .Headers.Add("X-CSRFToken", get_token)
                .Headers.Add("Origin", "https://www.instagram.com")
                .Headers.Add("Sec-Fetch-Site", "same-origin")
                .Headers.Add("Accept-Language", "en-US,en;q=0.9")
                .AutomaticDecompression = (DecompressionMethods.Deflate Or DecompressionMethods.GZip)
                .ContentLength = Bytes.Length
            End With
            Dim Stream As Stream = Request_Rest.GetRequestStream()
            Stream.Write(Bytes, 0, Bytes.Length)
            Stream.Close()
            Dim Responseee As HttpWebResponse = Request_Rest.GetResponse
            Dim Reader As New StreamReader(Responseee.GetResponseStream())
            Dim Stringss As String = Reader.ReadToEnd
            If Stringss.Contains("Thanks! Please check") Then
                Return ("Reset Done : " & Regex.Match(Stringss, "check  (.*) for").Groups(1).ToString)
            Else
                Return "Error From Instagram : " & Stringss
            End If
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
#End Region




    'RXLib v2.3 (Ready List)
    Public Function Get_Usernames_Following(username As String) As List(Of String)
        Dim userid As String = get_userid(username)
        Dim users As New List(Of String)
        Try
            If (Cookies.Length = 0) Then
            Else
                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & usfollowing & "&variables={""id"":""" & userid & """,""include_reel"":true,""fetch_mutual"":false,""first"":50}"), HttpWebRequest)
                httpPost.Method = "GET"
                httpPost.KeepAlive = True
                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)
                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)
                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd
                Dim matches As MatchCollection = Regex.Matches(Response, rgxusfls)
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
    'RXLib v2.3 (Ready List)
    Public Function Get_Usernames_Followers(username As String) As List(Of String)
        Dim userid As String = get_userid(username)
        Dim users As New List(Of String)
        Try
            If (Cookies.Length = 0) Then
            Else
                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & usfollowers & "&variables={""id"":""" & userid & """,""include_reel"":true,""fetch_mutual"":false,""first"":50}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)


                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd
                Dim matches As MatchCollection = Regex.Matches(Response, rgxusfls)

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
    'RXLib v2.3 (Response String)
    Public Function Get_Following_Response(username As String) As String
        Dim userid As String = get_userid(username)

        Try

            If (Cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & usfollowing & "&variables={""id"":""" & userid & """,""include_reel"":true,""fetch_mutual"":false,""first"":50}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)


                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd

                Return Response

            End If


        Catch ex As Exception

            Return ""
        End Try


    End Function
    'RXLib v2.3 (Response String)
    Public Function Get_Followers_Response(username As String) As String
        Dim userid As String = get_userid(username)

        Try

            If (Cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & usfollowers & "&variables={""id"":""" & userid & """,""include_reel"":true,""fetch_mutual"":false,""first"":50}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)


                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd

                Return Response

            End If


        Catch ex As Exception

            Return ""
        End Try


    End Function
    'RXLib v2.2


    'Old Version Fixed With in new version (v 2.3.1)

    'Function Edit_Profile(Optional first_name As String = "", Optional email As String = "", Optional username As String = "") As Boolean
    '    Try
    '        Dim csrftoken As String = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
    '        Dim postData As String = String.Format(edit, first_name, email, username)
    '        Dim en As New UTF8Encoding
    '        Dim byteData As Byte() = en.GetBytes(postData)
    '        Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/accounts/edit/"), HttpWebRequest)
    '        httpPost.Method = "POST"
    '        httpPost.KeepAlive = True
    '        httpPost.ContentType = "application/x-www-form-urlencoded"
    '        httpPost.UserAgent = user_agent
    '        httpPost.ContentLength = byteData.Length
    '        httpPost.Headers.Add("x-csrftoken", csrftoken)
    '        httpPost.Headers.Add("X-Instagram-AJAX", "1")
    '        httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
    '        httpPost.Headers.Add("Cookie", cookies)
    '        Dim poststr As Stream = httpPost.GetRequestStream()
    '        poststr.Write(byteData, 0, byteData.Length)
    '        poststr.Close()

    '        Dim POST_Response As HttpWebResponse
    '        POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)
    '        Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
    '        Dim Response As String = Post_Reader.ReadToEnd

    '        If Response.Contains("{""status"": ""ok""}") Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        Return False
    '    End Try

    'End Function

    'return full response String
    Public Function Get_Timeline() As String


        Try

            If (Cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & timeline & "&variables={""cached_feed_item_ids"":[],""fetch_media_item_count"":24,""has_stories"":false}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)


                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd


                Return Response


            End If


        Catch ex As Exception
            Return ""
        End Try


    End Function
    'return full response String
    Public Function Get_Hashtag(Hashtag_Name As String) As String
        Try

            If (Cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & hashtag & "&variables={""tag_name"":""" & Hashtag_Name & """,""show_ranked"":false,""first"":50}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)


                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd
                Return Response
            End If


        Catch ex As Exception
            Return ""
        End Try


    End Function
    'return full response String
    Public Function Ge_Explore() As String


        Try

            If (Cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & explore & "&variables={""first"":24}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)


                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd

                Return Response


            End If


        Catch ex As Exception
            Return ""

        End Try


    End Function
    Public Function Like_Comment_ID(Comment_ID As String) As Boolean
        Try

            If (Cookies.Length = 0) Then
                Return False
            Else

                Dim csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim postData As String = ""
                Dim tempcook As New CookieContainer
                Dim en As New UTF8Encoding
                Dim byteData As Byte() = en.GetBytes(postData)

                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/web/comments/like/" & Comment_ID & "/"), HttpWebRequest)

                httpPost.Method = "POST"
                httpPost.KeepAlive = True
                httpPost.ContentType = "application/x-www-form-urlencoded"
                httpPost.UserAgent = user_agent
                httpPost.ContentLength = byteData.Length
                httpPost.Headers.Add("x-csrftoken", csrftoken)
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)

                'Send Data
                Dim poststr As Stream = httpPost.GetRequestStream()
                poststr.Write(byteData, 0, byteData.Length)
                poststr.Close()

                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd


                If Response.Contains("{""status"": ""ok""}") Then
                    Return True
                Else
                    Return False
                End If
            End If


        Catch ex As Exception
            Return False
        End Try

    End Function
    'RXLib v2.1
    Public Function Like_Post_ID(Post_ID As String) As Boolean

        Try

            If (Cookies.Length = 0) Then
                Return False
            Else

                Dim csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim postData As String = ""
                Dim tempcook As New CookieContainer
                Dim en As New UTF8Encoding
                Dim byteData As Byte() = en.GetBytes(postData)

                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/web/likes/" & Post_ID & "/like/"), HttpWebRequest)

                httpPost.Method = "POST"
                httpPost.KeepAlive = True
                httpPost.ContentType = "application/x-www-form-urlencoded"
                httpPost.UserAgent = user_agent
                httpPost.ContentLength = byteData.Length
                httpPost.Headers.Add("x-csrftoken", csrftoken)
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)

                'Send Data
                Dim poststr As Stream = httpPost.GetRequestStream()
                poststr.Write(byteData, 0, byteData.Length)
                poststr.Close()

                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd


                If Response.Contains("{""status"": ""ok""}") Then
                    Return True
                Else
                    Return False
                End If
            End If


        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function UnLike_Post_ID(Post_ID As String) As Boolean

        Try
            If (Cookies.Length = 0) Then
                Return False
            Else

                Dim csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim postData As String = ""
                Dim tempcook As New CookieContainer
                Dim en As New UTF8Encoding
                Dim byteData As Byte() = en.GetBytes(postData)

                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/web/likes/" & Post_ID & "/unlike/"), HttpWebRequest)

                httpPost.Method = "POST"
                httpPost.KeepAlive = True
                httpPost.ContentType = "application/x-www-form-urlencoded"
                httpPost.UserAgent = user_agent
                httpPost.ContentLength = byteData.Length
                httpPost.Headers.Add("x-csrftoken", csrftoken)
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)

                'Send Data
                Dim poststr As Stream = httpPost.GetRequestStream()
                poststr.Write(byteData, 0, byteData.Length)
                poststr.Close()

                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd


                If Response.Contains("{""status"": ""ok""}") Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function Comment_ID(Post_ID As String, Comment_Text As String) As Boolean
        Try

            If (Cookies.Length = 0) Then
                Return False
            Else

                Dim csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim postData As String = "comment_text=" & Comment_Text
                Dim tempcook As New CookieContainer
                Dim en As New UTF8Encoding
                Dim byteData As Byte() = en.GetBytes(postData)

                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/web/comments/" + Post_ID + "/add/"), HttpWebRequest)
                httpPost.Method = "POST"
                httpPost.KeepAlive = True
                httpPost.ContentType = "application/x-www-form-urlencoded"
                httpPost.UserAgent = user_agent
                httpPost.ContentLength = byteData.Length
                httpPost.Headers.Add("x-csrftoken", csrftoken)
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)

                Dim poststr As Stream = httpPost.GetRequestStream()
                poststr.Write(byteData, 0, byteData.Length)
                poststr.Close()

                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)

                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd


                If Response.Contains("""status"": ""ok""}") Then
                    Return True
                Else
                    Return False
                End If

            End If
        Catch ex As Exception
            Return False
        End Try




    End Function
    'RXLib v2.0
    Public Function Get_Usernames_Comments(Post_Shortcode As String) As List(Of String)

        Dim users As New List(Of String)
        Try

            If (Cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & frmcom & "&variables={""shortcode"":""" & Post_Shortcode & """,""first"":24}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)


                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd
                Dim matches As MatchCollection = Regex.Matches(Response, usrcom)

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
    Public Function Get_Usernames_Likes(Post_Shortcode As String) As List(Of String)


        Try
            Dim users As New List(Of String)

            If (Cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & frmlikes & "&variables={""shortcode"":""" & Post_Shortcode & """,""include_reel"":true,""first"":24}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)


                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd
                Dim matches As MatchCollection = Regex.Matches(Response, usrlk)

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
    Public Function Get_Posts_Timeline() As List(Of String)


        Try

            If (Cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & timeline & "&variables={""cached_feed_item_ids"":[],""fetch_media_item_count"":24,""has_stories"":false}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)


                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd

                Dim posts As New List(Of String)
                Dim matches As MatchCollection = Regex.Matches(Response, Postsrgx)
                For Each match As Match In matches
                    posts.Add(match.Groups(1).Value)
                Next
                Return posts


            End If


        Catch ex As Exception
            Dim errors As New List(Of String)
            Return errors
        End Try


    End Function
    Public Function Get_Posts_Hashtag(Hashtag_Name As String) As List(Of String)


        Try

            If (Cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & hashtag & "&variables={""tag_name"":""" & Hashtag_Name & """,""show_ranked"":false,""first"":50}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)


                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd
                Dim posts As New List(Of String)
                Dim matches As MatchCollection = Regex.Matches(Response, Postshashtag)
                For Each match As Match In matches
                    posts.Add(match.Groups(1).Value)
                Next
                Return posts

            End If


        Catch ex As Exception
            Dim errors As New List(Of String)
            Return errors
        End Try


    End Function
    Public Function Get_Posts_Explore() As List(Of String)


        Try

            If (Cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & explore & "&variables={""first"":24}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)


                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd
                Dim posts As New List(Of String)
                Dim matches As MatchCollection = Regex.Matches(Response, Postsrgx)
                For Each match As Match In matches
                    posts.Add(match.Groups(1).Value)
                Next
                Return posts


            End If


        Catch ex As Exception
            Dim errors As New List(Of String)
            Return errors
        End Try


    End Function
    Public Function Get_Account_Cookies(Username As String, Password As String) As String

        Try
            Dim csrftoken As String = get_token()


            Dim postData As String = "username=" & Username & "&password=" & Password
            Dim en As New UTF8Encoding
            Dim byteData As Byte() = en.GetBytes(postData)




            Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/accounts/login/ajax/"), HttpWebRequest)

            httpPost.Method = "POST"
            httpPost.KeepAlive = True
            httpPost.ContentType = "application/x-www-form-urlencoded"
            httpPost.UserAgent = user_agent
            httpPost.ContentLength = byteData.Length
            httpPost.Headers.Add("x-csrftoken", csrftoken)
            httpPost.Headers.Add("X-Instagram-AJAX", "1")
            httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")

            'Send Data
            Dim poststr As Stream = httpPost.GetRequestStream()
            poststr.Write(byteData, 0, byteData.Length)
            poststr.Close()

            'Get Response
            Dim POST_Response As HttpWebResponse
            POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)

            Dim tt As String
            tt = POST_Response.Headers("Set-Cookie")

            Cookies = Regex.Match(tt, "csrftoken=.*?;").Value & " " & Regex.Match(tt, "mid=.*?;").Value & " " & Regex.Match(tt, "ds_user_id=.*?;").Value & " " & Regex.Match(tt, "sessionid=.*?;").Value

            Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
            Dim Response As String = Post_Reader.ReadToEnd

            If Response.Contains("authenticated"": true") Then
                Return Cookies
            Else
                Return "Check Your Username & Password"
            End If
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function
    'RXLib v1.0
    Public Function Login(Username As String, Password As String) As Boolean
        Try
            Dim csrftoken As String = get_token()
            Dim postData As String = "username=" & Username & "&password=" & Password
            Dim tempcook As New CookieContainer
            Dim en As New UTF8Encoding
            Dim byteData As Byte() = en.GetBytes(postData)
            Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/accounts/login/ajax/"), HttpWebRequest)
            httpPost.Method = "POST"
            httpPost.KeepAlive = True
            httpPost.ContentType = "application/x-www-form-urlencoded"
            httpPost.UserAgent = user_agent
            httpPost.ContentLength = byteData.Length
            httpPost.Headers.Add("x-csrftoken", csrftoken)
            httpPost.Headers.Add("X-Instagram-AJAX", "1")
            httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")

            'Send Data
            Dim poststr As Stream = httpPost.GetRequestStream()
            poststr.Write(byteData, 0, byteData.Length)
            poststr.Close()

            'Get Response
            Dim POST_Response As HttpWebResponse
            POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)
            Dim tt As String = POST_Response.Headers("Set-Cookie")

            Cookies = Regex.Match(tt, "csrftoken=.*?;").Value & " " & Regex.Match(tt, "mid=.*?;").Value & " " & Regex.Match(tt, "ds_user_id=.*?;").Value & " " & Regex.Match(tt, "sessionid=.*?;").Value

            Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
            Dim Response As String = Post_Reader.ReadToEnd

            If Response.Contains("authenticated"": true") Then
                Dim httpPost1 = DirectCast(WebRequest.Create("https://www.instagram.com/accounts/edit/?__a=1"), HttpWebRequest)

                httpPost1.Method = "GET"
                httpPost1.KeepAlive = True
                httpPost1.UserAgent = user_agent
                httpPost1.Headers.Add("X-Instagram-AJAX", "1")
                httpPost1.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost1.Headers.Add("Cookie", Cookies)
                httpPost1.Referer = "https://www.instagram.com/accounts/edit/"


                'Get Response
                Dim POST_Response1 As HttpWebResponse
                POST_Response1 = DirectCast(httpPost1.GetResponse(), HttpWebResponse)


                Dim Post_Reader1 As New StreamReader(POST_Response1.GetResponseStream())
                Dim Response1 As String = Post_Reader1.ReadToEnd


                firstname = Regex.Match(Response1, "first_name"":""(.*?)""").Groups(1).Value
                email = Regex.Match(Response1, "email"":""(.*?)""").Groups(1).Value
                user = Regex.Match(Response1, "username"":""(.*?)""").Groups(1).Value
                Return True

            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try



    End Function
    Public Function Like_Post_Link(Post_Link As String) As Boolean

        Try

            If (Cookies.Length = 0) Then
                Return False
            Else
                Dim postid As String = get_postid(Post_Link)
                Dim csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim postData As String = ""
                Dim tempcook As New CookieContainer
                Dim en As New UTF8Encoding
                Dim byteData As Byte() = en.GetBytes(postData)

                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/web/likes/" & postid & "/like/"), HttpWebRequest)

                httpPost.Method = "POST"
                httpPost.KeepAlive = True
                httpPost.ContentType = "application/x-www-form-urlencoded"
                httpPost.UserAgent = user_agent
                httpPost.ContentLength = byteData.Length
                httpPost.Headers.Add("x-csrftoken", csrftoken)
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)

                'Send Data
                Dim poststr As Stream = httpPost.GetRequestStream()
                poststr.Write(byteData, 0, byteData.Length)
                poststr.Close()

                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd


                If Response.Contains("{""status"": ""ok""}") Then
                    Return True
                Else
                    Return False
                End If
            End If


        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function UnLike_Post_Link(Post_Link As String) As Boolean

        Try
            If (Cookies.Length = 0) Then
                Return False
            Else
                Dim postid As String = get_postid(Post_Link)
                Dim csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim postData As String = ""
                Dim tempcook As New CookieContainer
                Dim en As New UTF8Encoding
                Dim byteData As Byte() = en.GetBytes(postData)

                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/web/likes/" & postid & "/unlike/"), HttpWebRequest)

                httpPost.Method = "POST"
                httpPost.KeepAlive = True
                httpPost.ContentType = "application/x-www-form-urlencoded"
                httpPost.UserAgent = user_agent
                httpPost.ContentLength = byteData.Length
                httpPost.Headers.Add("x-csrftoken", csrftoken)
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)

                'Send Data
                Dim poststr As Stream = httpPost.GetRequestStream()
                poststr.Write(byteData, 0, byteData.Length)
                poststr.Close()

                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd


                If Response.Contains("{""status"": ""ok""}") Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function Follow(Username As String) As Boolean

        Try
            If (Cookies.Length = 0) Then
                Return False
            Else
                Dim userid As String = get_userid(Username)
                Dim csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim postData As String = ""
                Dim tempcook As New CookieContainer
                Dim en As New UTF8Encoding
                Dim byteData As Byte() = en.GetBytes(postData)

                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/web/friendships/" & userid & "/follow/"), HttpWebRequest)

                httpPost.Method = "POST"
                httpPost.KeepAlive = True
                httpPost.ContentType = "application/x-www-form-urlencoded"
                httpPost.UserAgent = user_agent
                httpPost.ContentLength = byteData.Length
                httpPost.Headers.Add("x-csrftoken", csrftoken)
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)

                'Send Data
                Dim poststr As Stream = httpPost.GetRequestStream()
                poststr.Write(byteData, 0, byteData.Length)
                poststr.Close()

                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd


                If Response.Contains("{""result"": ""following"", ""status"": ""ok""}") Then
                    Return True
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Return False
        End Try




    End Function
    Public Function UnFollow(Username As String) As Boolean
        Try
            If (Cookies.Length = 0) Then
                Return False
            Else
                Dim userid As String = get_userid(Username)
                Dim csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim postData As String = ""
                Dim tempcook As New CookieContainer
                Dim en As New UTF8Encoding
                Dim byteData As Byte() = en.GetBytes(postData)

                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/web/friendships/" & userid & "/unfollow/"), HttpWebRequest)

                httpPost.Method = "POST"
                httpPost.KeepAlive = True

                httpPost.ContentType = "application/x-www-form-urlencoded"
                httpPost.UserAgent = user_agent
                httpPost.ContentLength = byteData.Length
                httpPost.Headers.Add("x-csrftoken", csrftoken)
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)

                'Send Data
                Dim poststr As Stream = httpPost.GetRequestStream()
                poststr.Write(byteData, 0, byteData.Length)
                poststr.Close()

                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)


                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd


                If Response.Contains("{""status"": ""ok""}") Then
                    Return True
                Else
                    Return False
                End If

            End If
        Catch ex As Exception
            Return False
        End Try




    End Function
    Public Function Comment_Link(Post_Url As String, Comment_Text As String) As Boolean
        Try

            If (Cookies.Length = 0) Then
                Return False
            Else
                Dim media_id As String = get_postid(Post_Url)
                Dim csrftoken As String = Regex.Match(Cookies, "csrftoken=(.*?);").Groups(1).Value
                Dim postData As String = "comment_text=" & Comment_Text
                Dim tempcook As New CookieContainer
                Dim en As New UTF8Encoding
                Dim byteData As Byte() = en.GetBytes(postData)

                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/web/comments/" + media_id + "/add/"), HttpWebRequest)
                httpPost.Method = "POST"
                httpPost.KeepAlive = True
                httpPost.ContentType = "application/x-www-form-urlencoded"
                httpPost.UserAgent = user_agent
                httpPost.ContentLength = byteData.Length
                httpPost.Headers.Add("x-csrftoken", csrftoken)
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", Cookies)

                Dim poststr As Stream = httpPost.GetRequestStream()
                poststr.Write(byteData, 0, byteData.Length)
                poststr.Close()

                'Get Response
                Dim POST_Response As HttpWebResponse
                POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)

                Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
                Dim Response As String = Post_Reader.ReadToEnd


                If Response.Contains("""status"": ""ok""}") Then
                    Return True
                Else
                    Return False
                End If

            End If
        Catch ex As Exception
            Return False
        End Try




    End Function
    Public Function get_userid(Username As String) As String
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
    Public Function get_postid(Post_Url As String) As String
        Try
            Dim httpGet = DirectCast(WebRequest.Create(Post_Url), HttpWebRequest)
            httpGet.Method = "GET"
            httpGet.ContentType = "application/json"
            httpGet.Headers.Add("X-Instagram-AJAX", "1")
            httpGet.Headers.Add("x-requested-with", "XMLHttpRequest")

            Dim Get_Response As HttpWebResponse
            Get_Response = DirectCast(httpGet.GetResponse(), HttpWebResponse)

            Dim Get_Reader As New StreamReader(Get_Response.GetResponseStream())

            Return Regex.Match(Get_Reader.ReadToEnd, """__typename"":"".*?"",""id"":""(.*?)"",""sho").Groups(1).Value
        Catch ex As Exception
            Return ""
        End Try

    End Function
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
End Class
