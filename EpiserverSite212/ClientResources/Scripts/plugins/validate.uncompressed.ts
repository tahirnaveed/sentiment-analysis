
import { fetch, alert, Response } from '@ephox/dom-globals';



const validateResponse = (response: Response) => {
    if (!response.ok) {
        // tslint:disable-next-line: no-console
        console.log(response);
        throw new Error('Network response problem');
    }


    const contentType = response.headers.get('content-type');
    if (contentType && contentType.includes('application/json')) {
        return response.json();
    }

    throw new TypeError('Invalid response');
};


declare const tinymce: any;
interface IContentResult {
    input: string;
    score: number;
}
const handleError = (editor: any, input: string) => (ex: any) => {
    console.log(ex);
    alert('Any error occured => check console');

};

const showResult = (editor: any) => (response: IContentResult) => {

    let information: string;
    let msgType: string;

    if (response.score > 0.50) {
        information = 'Great work !!!';
        msgType = 'info';
    } else {
        information = 'You might consider rephrasing';
        msgType = 'warning';
    }
    editor.notificationManager.open({
        text: information,
        type: msgType,
        timeout: 5000
    });

};



const setup = (editor, url) => {
    editor.addButton('customplateplugin', {
        title: 'Validate',
        icon: 'checkmark',


        onClick: () => {
            // tslint:disable-next-line:no-console
            const input: string = editor.getBody().textContent;

            const inputUrl = '/api/ContentHelper/please-analyse?sentence=' + input;

            fetch(inputUrl)
                .then((response) => {
                    return response;
                })
                .then(validateResponse)
                .then(showResult(editor))
                .catch(handleError(editor, input));
        }
    });
};

export default () => {
    tinymce.PluginManager.add('customplateplugin', setup);
};
