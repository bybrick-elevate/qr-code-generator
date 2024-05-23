using QRCoder;

Console.WriteLine(@" $$$$$$\  $$$$$$$\         $$$$$$\                  $$\           ");
Console.WriteLine(@"$$  __$$\ $$  __$$\       $$  __$$\                 $$ |          ");
Console.WriteLine(@"$$ /  $$ |$$ |  $$ |      $$ /  \__| $$$$$$\   $$$$$$$ | $$$$$$\  ");
Console.WriteLine(@"$$ |  $$ |$$$$$$$  |      $$ |      $$  __$$\ $$  __$$ |$$  __$$\ ");
Console.WriteLine(@"$$ |  $$ |$$  __$$<       $$ |      $$ /  $$ |$$ /  $$ |$$$$$$$$ |");
Console.WriteLine(@"$$ $$\$$ |$$ |  $$ |      $$ |  $$\ $$ |  $$ |$$ |  $$ |$$   ____|");
Console.WriteLine(@"\$$$$$$ / $$ |  $$ |      \$$$$$$  |\$$$$$$  |\$$$$$$$ |\$$$$$$$\ ");
Console.WriteLine(@" \___$$$\ \__|  \__|       \______/  \______/  \_______| \_______|");
Console.WriteLine(@"     \___|");
Console.WriteLine(@"                                                            $$\                         ");
Console.WriteLine(@"                                                            $$ |                        ");
Console.WriteLine(@" $$$$$$\   $$$$$$\  $$$$$$$\   $$$$$$\   $$$$$$\  $$$$$$\ $$$$$$\    $$$$$$\   $$$$$$\  ");
Console.WriteLine(@"$$  __$$\ $$  __$$\ $$  __$$\ $$  __$$\ $$  __$$\ \____$$\\_$$  _|  $$  __$$\ $$  __$$\ ");
Console.WriteLine(@"$$ /  $$ |$$$$$$$$ |$$ |  $$ |$$$$$$$$ |$$ |  \__|$$$$$$$ | $$ |    $$ /  $$ |$$ |  \__|");
Console.WriteLine(@"$$ |  $$ |$$   ____|$$ |  $$ |$$   ____|$$ |     $$  __$$ | $$ |$$\ $$ |  $$ |$$ |      ");
Console.WriteLine(@"\$$$$$$$ |\$$$$$$$\ $$ |  $$ |\$$$$$$$\ $$ |     \$$$$$$$ | \$$$$  |\$$$$$$  |$$ |      ");
Console.WriteLine(@" \____$$ | \_______|\__|  \__| \_______|\__|      \_______|  \____/  \______/ \__|      ");
Console.WriteLine(@"$$\   $$ |                                                                              ");
Console.WriteLine(@"\$$$$$$  |                                                                              ");
Console.WriteLine(@" \______/");
Console.WriteLine("");

Console.WriteLine("Please enter URL:");
var url = Console.ReadLine();

Console.WriteLine("Please enter filename (without .png)");
var fileName = Console.ReadLine();

Console.WriteLine("Generating QR-code...");

var qrGenerator = new QRCodeGenerator();
var urlPayload = new PayloadGenerator.Url(url);
var qrCodeData = qrGenerator.CreateQrCode(urlPayload);

var qrCode = new PngByteQRCode(qrCodeData).GetGraphic(512);


var dirPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
var filePath = $"{dirPath}/{fileName}.png";

var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
fs.Write(qrCode, 0, qrCode.Length);

Console.WriteLine($"Done!");
Console.WriteLine($"File can be found at {filePath}");