var express = require('express');
var router = express.Router();
var dataObj = require("./gameData.js");

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('options', {});
});

router.post('/', function(req, res, next){
    var x = req.body;
    console.log(req.body.mode);
    console.log(req.body.name);
    dataObj.putData(req.body);
    console.log(dataObj.getData());
    res.render('options', {});
  });

module.exports = router;