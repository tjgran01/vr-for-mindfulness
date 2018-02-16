var express = require('express');
var router = express.Router();

router.get('/', function(req, res, next) {
    console.log(req.user);
    console.log(req);
    if(req.user!=null){
        
        res.render('dashboard', {user : req.user});
    }  
    else
        res.redirect('/');
});

module.exports = router;