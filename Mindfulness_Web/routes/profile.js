var express = require('express');
var router = express.Router();

router.get('/', function(req, res, next) {
    console.log(req.user);
    if(req.user!=null){
        
        if(req.session.views){
            req.session.views++;
            console.log("Number of views" + req.session.views);
        }else{
            req.session.views = 1;
            console.log("New session view in progress");
        }
        res.render('profile', { user : req.user });
    }  
    else
        res.redirect('/');
});

module.exports = router;