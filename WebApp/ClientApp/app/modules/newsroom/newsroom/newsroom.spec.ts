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

    it('should create Component', () => {
        expect(this.spec.instance instanceof NewsRoomComponent).toBe(true, 'should create Component');
    });
});