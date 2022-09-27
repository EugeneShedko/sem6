function OnClickHeader(x,y,z)
{
        var patern = new RegExp(/^[0-9]*\.?[0-9]+$/);
        if (patern.test(x.value) && patern.test(y.value))
        {
            if (req)
            {
                req.open("POST","http://localserver33114sea:8080/AS_33114SEA/SssHeader", true);
                req.onreadystatechange = function()
                {
                    if (req.readyState == 4)
                    {
                        if (req.status == 200)
                        {
                            z.value=req.getResponseHeader("Value-Z");
                        }
                        else
                            alert("status = "+ req.status +"\n"+req.statusText);
                        }
                };
                req.setRequestHeader("Value-X",  x.value);
                req.setRequestHeader("Value-Y",  y.value);
                req.send("null");
            }
        }
        else alert ("error");
};
