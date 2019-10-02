file1_sep = "v1.1/aglina/relax/Frame (ID).png"
file2_sep = "v1.1/aglina/interact/Frame (ID).png"
for i=1, 61 do
	LoadBitmap((string.gsub(file1_sep, "ID", i)))
end
for i=1, 31 do
	LoadBitmap((string.gsub(file2_sep, "ID", i)))
end

isClick = false

function OnClick()
isClick = true
end
RequestClickEvent("OnClick")

i=0
while true do
	DisplayBitmap(i)
	Sleep(33)
	i=i+1
	if(i > 92) then i = 0 end
	if(i == 61) then i = 1 end
	if(isClick and i < 61) then
		i = 61
		isClick = false
	end
end