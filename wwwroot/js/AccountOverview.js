const params = new URLSearchParams(window.location.search)
const LoLName = params.get("lolName")
const Region = params.get("region")



try {
    SetAccountData(LoLName, Region)
} catch (e) {
    console.log(e)
}

async function GetAccountOverview(lolName, region) {
    let apistring = "/api/accountoverview/" + encodeURIComponent(lolName) + "?region=" + encodeURIComponent(region)
    let response = await fetch(apistring)
    if (!response.ok) throw new Error("failed status code:" + response.status)
    let accountOverview = await response.json()
    return accountOverview
}
async function SetAccountData(lolName, region) {
    let accountOverview = await GetAccountOverview(lolName, region)
    document.getElementById("lolName").innerText = accountOverview.gameName + "#" + accountOverview.tagLine
    document.getElementById("summonerLevel").innerText = "lvl " + accountOverview.summonerLevel
    const img = document.getElementById("profileIcon")
    img.src = "/api/profileicon/" + accountOverview.profileIconId
    img.style.visibility = "visible"
    console.log(accountOverview)
}
