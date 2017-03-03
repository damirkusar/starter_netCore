import { } from 'jasmine';
import { MainSpec } from '../../../main.spec';
import { NewsRoomComponent } from './newsroom.component';

import { NewsService } from '../services/news.service';
import { NewsResolverService } from '../services/newsResolver.service';

describe('NewsRoomComponent', () => {
    beforeEach(() => {
        this.spec = new MainSpec();
        this.spec.init(NewsRoomComponent, null, null, [NewsService, NewsResolverService]);
    });

    afterEach(() => {
        this.spec.destroy();
    });

    it('should create Component', () => {
        expect(this.spec.instance instanceof NewsRoomComponent).toBeTruthy();
    });

    it('news property undefined', () => {
        expect(this.spec.instance.news).toBeUndefined();
    });
});