var express = require('express');
var router = express.Router();
var dataObj = require("./gameData.js");

/* GET home page. */
router.get('/', function(req, res, next) {
  //console.log(dataObj.getData());
  var x = dataObj.getData();
  res.json({ "mode":x.mode, "audioTrack":x.audio, "audioVolume":x.audioVolume, "backgroundVolume":x.backgroundVolume, "breathing":x.breathing });
  //res.send(dataObj.getData());
  //res.render('capture', {});  //'index' is the index.ejs that has to be used
});

module.exports = router;