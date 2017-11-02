var express = require('express');
var router = express.Router();
var dataObj = require("./gameData.js");

/* GET home page. */
router.get('/', function(req, res, next) {
  console.log(dataObj.getData());
  var x = dataObj.getData();
  res.json({ "user": x.name, "mode":x.mode });
  //res.send(dataObj.getData());
  //res.render('capture', {});  //'index' is the index.ejs that has to be used
});

module.exports = router;