var mongoose = require('mongoose');
var Schema = mongoose.Schema;
var passportLocalMongoose = require('passport-local-mongoose');

var ClinicianSchema = new Schema({
    username: String,           //Email ID
    password: String,           //excrypted through passport module
    location: String,
    name: String,
    age: Number,
    patients:[{
        user_id:{ 
            type: mongoose.Schema.Types.ObjectId, 
            ref: 'User',
        }
    }],
    isClinician: Boolean
},
{
    collection: 'clinicians'
  });

  ClinicianSchema.plugin(passportLocalMongoose);

module.exports = mongoose.model('Clinician', ClinicianSchema);
