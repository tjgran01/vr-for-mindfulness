var express = require('express');
var router = express.Router();
var dataObj = require("./gameData.js");

/* GET home page. */
router.get('/', function(req, res, next) {
  var x = dataObj.getData();
  res.render('options', {playerName:x.name, timeMode:x.mode});
});

router.post('/', function(req, res, next){
    var x = req.body;
    dataObj.putData(req.body);

    var y = dataObj.getData();
    //console.log(dataObj.getData());
    res.render('options', {playerName:y.name, timeMode:y.mode});
  });

module.exports = router;