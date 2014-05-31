var http = require('http');
var zlib = require('zlib');

var request = http.get('http://www.cs.washington.edu/research/xmldatasets/data/SwissProt/SwissProt.xml.gz');

request.on('response', function(response) {
	response.pipe(zlib.createGunzip()).pipe(process.stdout);
});
