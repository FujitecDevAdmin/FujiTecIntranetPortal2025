// Retrieve combined image data and descriptions from the hidden field
const newsRendererDataField = document.querySelector('#newsRendererData');
let newsRendererList = [];

if (newsRendererDataField !== null && newsRendererDataField.value !== '') {
    const newsRendererData = newsRendererDataField.value;
    newsRendererList = JSON.parse(newsRendererData);
} else {
    // Handle the case when data is null or empty
    console.log('News Renderer Data is null or empty.');
}

let topNewsRenderCount = 15;

function truncateString(stringInput, length) {
    return stringInput.length > length ?
        `${stringInput.slice(0, length)}...` :
        stringInput;
}

function formatDate(date) {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Month is zero-indexed
    const year = date.getFullYear();

    return `${day}-${month}-${year}`;
}


// Define the renderNews function
function renderNews(newsData, containerId) {
    const newsContainer = document.querySelector('.news-outer-container .news-inner-container');

    if (!newsContainer) {
        console.error("Container not found");
        return;
    }

    if (!Array.isArray(newsData)) {
        console.error("News data is not an array");
        return;
    }

    // Assuming newsData is an array of news articles with 'Title' and 'NewsDate' properties
    const filteredNews = newsData.filter(article => article.Title.toLowerCase().includes('fujitec'));

    // Sort the filtered news by NewsDate from latest to oldest
    filteredNews.sort((a, b) => new Date(b.NewsDate) - new Date(a.NewsDate));

    // Take the top 10 news articles
    const top10News = filteredNews.slice(0, topNewsRenderCount);

    top10News.forEach(news => {

        const newsTitle = truncateString(news.Title, 200);
        const authorName = truncateString(news.Author, 15);
        var parsedDate = new Date(news.NewsDate);

        const newsCard = document.createElement("a");
        newsCard.href = news.TitleLink;
        newsCard.target = "_blank";
        newsCard.className = "news-card";

        const line = document.createElement("div");
        line.id = "line";

        const img = document.createElement("img");
        img.src = news.ImageSource;
        img.alt = news.ImageAlt;

        const newsInfo = document.createElement("div");
        newsInfo.className = "news-info";

        const title = document.createElement("h2");
        title.className = "news-title";
        title.innerHTML = newsTitle;

        const authorDate = document.createElement("h4");
        authorDate.className = "news-author-date";
        authorDate.innerHTML = `${authorName} - ${formatDate(parsedDate)}`;

        newsInfo.appendChild(title);
        newsInfo.appendChild(authorDate);

        newsCard.appendChild(line);
        newsCard.appendChild(img);
        newsCard.appendChild(newsInfo);

        newsContainer.appendChild(newsCard);

    });
}

function newsRendererFunction() {
    if (newsRendererList.length > 0) {
        renderNews(newsRendererList, 'newsContainer');
    } else {
        console.log('No news data available for rendering.');
    }
}


// Expose newsRendererFunction globally
window.newsRendererFunction = newsRendererFunction;
