var mongoose = require('mongoose');
var Schema = mongoose.Schema;
var passportLocalMongoose = require('passport-local-mongoose');

var UserSchema = new Schema({
    username: String,           //Email ID
    password: String,           //excrypted through passport module
    location: String,
    name: String,
    clinician_id: { 
        type: mongoose.Schema.Types.ObjectId, 
        ref: 'Clinician'
    },
    age: Number,
    authentication_id: {            //Auto generater when regsitration
        type: mongoose.Schema.Types.ObjectId, 
        ref: 'AuthenticationID'
    },
    isUser:Boolean   
},
{
    collection: 'users'
  });

UserSchema.plugin(passportLocalMongoose);

module.exports = mongoose.model('User', UserSchema);
