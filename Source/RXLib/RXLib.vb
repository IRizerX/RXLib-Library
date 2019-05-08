Imports System.Net
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Text
Imports RXLib.Hashes
Imports RXLib.rgx
Imports RXLib.Headers
Imports RXLib.Posts
Public Class RXLib


    Private Shared cookies As String


    'RXLib v2.2



    Function Edit_Profile(Optional first_name As String = "", Optional email As String = "", Optional username As String = "") As Boolean
        Try
            Dim csrftoken As String = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
            Dim postData As String = String.Format(edit, first_name, email, username)
            Dim en As New UTF8Encoding
            Dim byteData As Byte() = en.GetBytes(postData)
            Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/accounts/edit/"), HttpWebRequest)
            httpPost.Method = "POST"
            httpPost.KeepAlive = True
            httpPost.ContentType = "application/x-www-form-urlencoded"
            httpPost.UserAgent = user_agent
            httpPost.ContentLength = byteData.Length
            httpPost.Headers.Add("x-csrftoken", csrftoken)
            httpPost.Headers.Add("X-Instagram-AJAX", "1")
            httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
            httpPost.Headers.Add("Cookie", cookies)
            Dim poststr As Stream = httpPost.GetRequestStream()
            poststr.Write(byteData, 0, byteData.Length)
            poststr.Close()

            Dim POST_Response As HttpWebResponse
            POST_Response = DirectCast(httpPost.GetResponse(), HttpWebResponse)
            Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
            Dim Response As String = Post_Reader.ReadToEnd

            If Response.Contains("{""status"": ""ok""}") Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    'return full response String
    Public Function Get_Timeline() As String


        Try

            If (cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & timeline & "&variables={""cached_feed_item_ids"":[],""fetch_media_item_count"":24,""has_stories"":false}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", cookies)


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

            If (cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & hashtag & "&variables={""tag_name"":""" & Hashtag_Name & """,""show_ranked"":false,""first"":50}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", cookies)


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

            If (cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & explore & "&variables={""first"":24}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", cookies)


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

            If (cookies.Length = 0) Then
                Return False
            Else

                Dim csrftoken As String = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
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
                httpPost.Headers.Add("Cookie", cookies)

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

            If (cookies.Length = 0) Then
                Return False
            Else

                Dim csrftoken As String = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
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
                httpPost.Headers.Add("Cookie", cookies)

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
            If (cookies.Length = 0) Then
                Return False
            Else

                Dim csrftoken As String = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
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
                httpPost.Headers.Add("Cookie", cookies)

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

            If (cookies.Length = 0) Then
                Return False
            Else

                Dim csrftoken As String = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
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
                httpPost.Headers.Add("Cookie", cookies)

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

            If (cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & frmcom & "&variables={""shortcode"":""" & Post_Shortcode & """,""first"":24}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", cookies)


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

            If (cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & frmlikes & "&variables={""shortcode"":""" & Post_Shortcode & """,""include_reel"":true,""first"":24}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", cookies)


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

            If (cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & timeline & "&variables={""cached_feed_item_ids"":[],""fetch_media_item_count"":24,""has_stories"":false}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", cookies)


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

            If (cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & hashtag & "&variables={""tag_name"":""" & Hashtag_Name & """,""show_ranked"":false,""first"":50}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", cookies)


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

            If (cookies.Length = 0) Then

            Else


                Dim httpPost = DirectCast(WebRequest.Create("https://www.instagram.com/graphql/query/?query_hash=" & explore & "&variables={""first"":24}"), HttpWebRequest)

                httpPost.Method = "GET"
                httpPost.KeepAlive = True

                httpPost.UserAgent = user_agent
                httpPost.Headers.Add("X-Instagram-AJAX", "1")
                httpPost.Headers.Add("x-requested-with", "XMLHttpRequest")
                httpPost.Headers.Add("Cookie", cookies)


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

            cookies = Regex.Match(tt, "csrftoken=.*?;").Value & " " & Regex.Match(tt, "mid=.*?;").Value & " " & Regex.Match(tt, "ds_user_id=.*?;").Value & " " & Regex.Match(tt, "sessionid=.*?;").Value

            Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
            Dim Response As String = Post_Reader.ReadToEnd

            If Response.Contains("authenticated"": true") Then
                Return cookies
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

            Dim tt As String
            tt = POST_Response.Headers("Set-Cookie")

            cookies = Regex.Match(tt, "csrftoken=.*?;").Value & " " & Regex.Match(tt, "mid=.*?;").Value & " " & Regex.Match(tt, "ds_user_id=.*?;").Value & " " & Regex.Match(tt, "sessionid=.*?;").Value

            Dim Post_Reader As New StreamReader(POST_Response.GetResponseStream())
            Dim Response As String = Post_Reader.ReadToEnd

            If Response.Contains("authenticated"": true") Then
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

            If (cookies.Length = 0) Then
                Return False
            Else
                Dim postid As String = get_postid(Post_Link)
                Dim csrftoken As String = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
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
                httpPost.Headers.Add("Cookie", cookies)

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
            If (cookies.Length = 0) Then
                Return False
            Else
                Dim postid As String = get_postid(Post_Link)
                Dim csrftoken As String = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
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
                httpPost.Headers.Add("Cookie", cookies)

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
            If (cookies.Length = 0) Then
                Return False
            Else
                Dim userid As String = get_userid(Username)
                Dim csrftoken As String = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
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
                httpPost.Headers.Add("Cookie", cookies)

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
            If (cookies.Length = 0) Then
                Return False
            Else
                Dim userid As String = get_userid(Username)
                Dim csrftoken As String = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
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
                httpPost.Headers.Add("Cookie", cookies)

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

            If (cookies.Length = 0) Then
                Return False
            Else
                Dim media_id As String = get_postid(Post_Url)
                Dim csrftoken As String = Regex.Match(cookies, "csrftoken=(.*?);").Groups(1).Value
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
                httpPost.Headers.Add("Cookie", cookies)

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
            httpGet.
            Headers.Add("x-requested-with", "XMLHttpRequest")

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
            httpGet.
        Headers.Add("x-requested-with", "XMLHttpRequest")

            Dim Get_Response As HttpWebResponse
            Get_Response = DirectCast(httpGet.GetResponse(), HttpWebResponse)

            Dim Get_Reader As New StreamReader(Get_Response.GetResponseStream())

            Return Regex.Match(Get_Reader.ReadToEnd, """csrf_token"":""(\w+)""").Groups(1).Value

        Catch ex As Exception
            Return ""
        End Try


    End Function


End Class
