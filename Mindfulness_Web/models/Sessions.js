var mongoose = require('mongoose');
var Schema = mongoose.Schema;

var SessionSchema = new Schema({
    _id: { 
        type: mongoose.Schema.Types.ObjectId, 
        ref: 'User'
    },
    username: String,
    sessions:[{
        session_id: String,
        date: Date,
        scene: String,
        audio: String,
        date: Date,
        start_time: String,
        end_time: String,
        duration: Number
        //Health readings //more to come
    }]
   
},
{
    collection: 'sessions'
  });

module.exports = mongoose.model('Sessions', SessionSchema);