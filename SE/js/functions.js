function CheckForm() {
	if (document.frmSE.QuerySearch.value=="")
	{
		alert("Enter keyword to search");
		document.frmSE.QuerySearch.focus();
		return false;
	}
	return true
}

function Show(divId)
{
    document.getElementById(divId).style.visible ='visible';
}

function Hide(divId)
{
    document.getElementById(divId).style.visible ='hidden';
}

function konfirmo() {
    return confirm('Jeni te sigurte \nper veprimin?')
}

function info() {
    return alert('Veprimi u krye!\n Ju lutem prisni derisa te kopjohen materialet!');
}