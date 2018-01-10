//全部替换
String.prototype.replaceAll = function (oldstr, newstr) {
    return this.replace(new RegExp(oldstr, "gm"), newstr);
}

var octOcean_exp = {
    //自定义全部替换
    replaceAll: function (str, oldstr, newstr) {
      return  str.replace(new RegExp(oldstr, "gm"), newstr)
        
    }
};