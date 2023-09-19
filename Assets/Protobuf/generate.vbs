path = createobject("Scripting.FileSystemObject").GetFolder(".").Path
set oShell = CreateObject("WScript.Shell")
m = "protoc -I=" & path & " --csharp_out=" & path & " " & path & "/*.proto"

Set objShell = WScript.CreateObject("WScript.Shell")
intReturn = objShell.Run(m, 0, True)

If intReturn > 0 Then
MsgBox "error"
Else
MsgBox "done"
End If

Set fso = CreateObject("scripting.FileSystemObject")
Set f = fso.GetFolder(path)
Set fc = f.Files

N = 0

line = ""
For Each uu In fc
t = uu.Path
If Instr(len(t)-3,t, ".cs") > 0 Then
line = line & t & vbCrLf 
set file = fso.opentextfile(t)
s = replace(file.readall,"Sky9Th","Sky9th")
file.close
set r = fso.opentextfile(t,2,true)
r.write s
End If
Next

MsgBox line


set cmdFile = fso.opentextfile(path & "/script.cmd",2,true)
cmdFile.write m & vbCrLf & "pause"