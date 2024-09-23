using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
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

string? input = null;
while (input == null)
{
	Console.WriteLine("Please enter URL (type CSV to read from CSV file instead)");
	input = Console.ReadLine();
	var useCsv = input?.ToUpper() == "CSV";

	if (input == null) continue;

	Dictionary<string, string> urls = [];
	if (useCsv)
	{
		Console.WriteLine("The csv file should be formated: filename (without .png), url");
		Console.WriteLine("Please enter path to CSV file");
		input = Console.ReadLine();
		var filePath = input ?? string.Empty;

		var config = new CsvConfiguration(CultureInfo.InvariantCulture)
		{
			HasHeaderRecord = false,
			NewLine = Environment.NewLine
		};
		var reader = new StreamReader(filePath);
		var csv = new CsvReader(reader, config);
		var records = csv.GetRecords<CsvRow>().ToList();

		foreach (var record in records)
		{
			if (record.FileName == null) continue;
			if (record.Url == null) continue;

			urls.Add(record.FileName, record.Url);
		}
	}
	else
	{
		Console.WriteLine("Please enter filename (without .png)");
		var fileName = Console.ReadLine() ?? string.Empty;

		urls.Add(fileName, input);
	}


	Console.WriteLine(urls.Count == 1 ? "Generating QR-code..." : "Generating QR-codes...");

	var dirPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/QR Codes";
	try
	{
		Directory.CreateDirectory(dirPath);
	}
	catch
	{
		// Ignore
	}

	foreach (var url in urls)
	{
		var qrGenerator = new QRCodeGenerator();
		var urlPayload = new PayloadGenerator.Url(url.Value);
		var qrCodeData = qrGenerator.CreateQrCode(urlPayload);

		var qrCode = new PngByteQRCode(qrCodeData).GetGraphic(512);

		var filePath = $"{dirPath}/{url.Key}.png";

		var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
		fs.Write(qrCode, 0, qrCode.Length);
		fs.Close();

		if (urls.Count == 1) Console.WriteLine($"File can be found at {filePath}");
	}

	if (urls.Count > 1) Console.WriteLine($"Files can be found at {dirPath}");
}

Console.WriteLine($"Done!");


public class CsvRow
{
	[Index(0)] public string? FileName { get; set; }

	[Index(1)] public string? Url { get; set; }
}