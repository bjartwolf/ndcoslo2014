var http = require('http');
var fs = require('fs');

http.createServer(function (req, res) {
	    res.writeHead(200, {'Content-Type': 'application/xml',
		                            'Content-Encoding':'gzip'});  
	    fs.createReadStream('SwissProt.xml.gz').pipe(res);
}).listen(1337, '127.0.0.1');

